using Tasky.Domain.Entities;
using Tasky.Domain.Enums;
using Tasky.Domain.Exceptions;

namespace Tasky.Domain.Tests;

public class BoardEntityTests
{
    // --- Create ---

    [Fact]
    public void Create_WithValidData_SetsProperties()
    {
        var ownerId = Guid.NewGuid();

        var board = BoardEntity.Create(ownerId, "Sprint 1", "Opis", BoardType.Kanban);

        Assert.NotEqual(Guid.Empty, board.BoardId);
        Assert.Equal(ownerId, board.OwnerId);
        Assert.Equal("Sprint 1", board.Name);
        Assert.Equal("Opis", board.Description);
        Assert.Equal(BoardType.Kanban, board.Type);
        Assert.Empty(board.Tasks);
        Assert.Equal(board.CreationDate, board.ModificationDate);
        Assert.Equal(DateTimeKind.Utc, board.CreationDate.Kind);
    }

    [Fact]
    public void Create_WithEmptyOwner_Throws()
        => Assert.Throws<InvalidBoardException>(
            () => BoardEntity.Create(Guid.Empty, "Sprint 1", null, BoardType.Kanban));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    public void Create_WithBlankName_Throws(string? name)
        => Assert.Throws<InvalidBoardException>(
            () => BoardEntity.Create(Guid.NewGuid(), name!, null, BoardType.Kanban));

    [Fact]
    public void Create_TrimsName()
    {
        var board = BoardEntity.Create(Guid.NewGuid(), "  Sprint 1  ", null, BoardType.Kanban);

        Assert.Equal("Sprint 1", board.Name);
    }

    // --- Update / Rename / ChangeDescription ---

    [Fact]
    public void Update_ChangesNameAndDescription()
    {
        var board = TestData.NewBoard();

        board.Update("  Sprint 2  ", "Nowy opis");

        Assert.Equal("Sprint 2", board.Name);
        Assert.Equal("Nowy opis", board.Description);
    }

    [Fact]
    public void Update_WithBlankName_ThrowsAndLeavesBoardUnchanged()
    {
        var board = BoardEntity.Create(Guid.NewGuid(), "Sprint 1", "Opis", BoardType.Kanban);

        Assert.Throws<InvalidBoardException>(() => board.Update("  ", "Inny opis"));

        Assert.Equal("Sprint 1", board.Name);
        Assert.Equal("Opis", board.Description);
    }

    [Fact]
    public void Rename_WithBlankName_Throws()
        => Assert.Throws<InvalidBoardException>(() => TestData.NewBoard().Rename(" "));

    [Fact]
    public void ChangeDescription_WithNull_ClearsDescription()
    {
        var board = BoardEntity.Create(Guid.NewGuid(), "Sprint 1", "Opis", BoardType.Kanban);

        board.ChangeDescription(null);

        Assert.Null(board.Description);
    }

    [Fact]
    public void EveryChange_MovesModificationDateForward()
    {
        var board = TestData.NewBoard();
        var previous = board.ModificationDate;
        TaskEntity? task = null;

        void AssertTouched(Action change)
        {
            TestClock.Tick();
            change();
            Assert.True(board.ModificationDate > previous);
            previous = board.ModificationDate;
        }

        AssertTouched(() => board.Rename("Nowa nazwa"));
        AssertTouched(() => board.ChangeDescription("Opis"));
        AssertTouched(() => board.Update("Inna nazwa", null));
        AssertTouched(() => task = board.AddTask("A", null, null, null, null));
        AssertTouched(() => board.ChangeTaskStatus(task!.TaskId, Status.Completed));
        AssertTouched(() => board.RemoveTask(task!.TaskId));
    }

    // --- AddTask / GetTask / RemoveTask ---

    [Fact]
    public void AddTask_CreatesToDoTaskOnThisBoard()
    {
        var board = TestData.NewBoard();

        var task = board.AddTask("  Setup CI  ", "Opis", null, 3, null);

        Assert.Equal("Setup CI", task.Title);
        Assert.Equal(board.BoardId, task.BoardId);
        Assert.Equal(Status.ToDo, task.Status);
        Assert.Equal(3, task.Estimate);
        Assert.Contains(task, board.Tasks);
    }

    [Fact]
    public void AddTask_AssignsIncreasingPositions()
    {
        var board = TestData.NewBoard();

        var positions = Enumerable.Range(0, 3)
            .Select(i => board.AddTask($"Task {i}", null, null, null, null).Position)
            .ToList();

        Assert.Equal(new[] { 0, 1, 2 }, positions);
    }

    [Fact]
    public void AddTask_AfterRemovingOne_ContinuesAfterHighestPosition()
    {
        var board = TestData.NewBoard();
        board.AddTask("A", null, null, null, null);
        var middle = board.AddTask("B", null, null, null, null);
        board.AddTask("C", null, null, null, null);
        board.RemoveTask(middle.TaskId);

        var next = board.AddTask("D", null, null, null, null);

        Assert.Equal(3, next.Position);
    }

    [Fact]
    public void AddTask_WithInvalidData_ThrowsAndAddsNothing()
    {
        var board = TestData.NewBoard();

        Assert.Throws<InvalidTaskException>(() => board.AddTask(" ", null, null, null, null));
        Assert.Throws<InvalidTaskException>(() => board.AddTask("Title", null, null, -1, null));

        Assert.Empty(board.Tasks);
    }

    [Fact]
    public void AddTask_WithZeroEstimate_IsAllowed()
    {
        var task = TestData.NewBoard().AddTask("Title", null, null, 0, null);

        Assert.Equal(0, task.Estimate);
    }

    [Fact]
    public void GetTask_WithUnknownId_Throws()
        => Assert.Throws<TaskNotFoundException>(() => TestData.NewBoard().GetTask(Guid.NewGuid()));

    [Fact]
    public void RemoveTask_RemovesOnlyThatTask()
    {
        var board = TestData.NewBoard();
        var keep = board.AddTask("Keep", null, null, null, null);
        var remove = board.AddTask("Remove", null, null, null, null);

        board.RemoveTask(remove.TaskId);

        Assert.Equal(new[] { keep }, board.Tasks);
    }

    [Fact]
    public void RemoveTask_WithUnknownId_Throws()
        => Assert.Throws<TaskNotFoundException>(() => TestData.NewBoard().RemoveTask(Guid.NewGuid()));

    // --- ChangeTaskStatus: reguła typu tablicy ---

    public static IEnumerable<object[]> AllStatuses()
        => Enum.GetValues<Status>().Select(s => new object[] { s });

    public static IEnumerable<object[]> StatusesRejectedOnListBoard()
        => Enum.GetValues<Status>()
            .Where(s => s is not (Status.ToDo or Status.Completed))
            .Select(s => new object[] { s });

    [Theory]
    [MemberData(nameof(AllStatuses))]
    public void ChangeTaskStatus_OnKanbanBoard_AllowsEveryStatus(Status status)
    {
        var board = TestData.NewBoard(BoardType.Kanban);
        var task = board.AddTask("A", null, null, null, null);

        board.ChangeTaskStatus(task.TaskId, status);

        Assert.Equal(status, task.Status);
    }

    [Theory]
    [InlineData(Status.ToDo)]
    [InlineData(Status.Completed)]
    public void ChangeTaskStatus_OnListBoard_AllowsToDoAndCompleted(Status status)
    {
        var board = TestData.NewBoard(BoardType.List);
        var task = board.AddTask("A", null, null, null, null);

        board.ChangeTaskStatus(task.TaskId, status);

        Assert.Equal(status, task.Status);
    }

    [Theory]
    [MemberData(nameof(StatusesRejectedOnListBoard))]
    public void ChangeTaskStatus_OnListBoard_RejectsOtherStatuses(Status status)
    {
        var board = TestData.NewBoard(BoardType.List);
        var task = board.AddTask("A", null, null, null, null);

        Assert.Throws<InvalidTaskException>(() => board.ChangeTaskStatus(task.TaskId, status));

        Assert.Equal(Status.ToDo, task.Status);
    }

    // --- ChangeTaskStatus: CompletedDate ---

    [Fact]
    public void ChangeTaskStatus_ToCompleted_SetsUtcCompletedDate()
    {
        var board = TestData.NewBoard();
        var task = board.AddTask("A", null, null, null, null);
        Assert.Null(task.CompletedDate);

        board.ChangeTaskStatus(task.TaskId, Status.Completed);

        Assert.NotNull(task.CompletedDate);
        Assert.Equal(DateTimeKind.Utc, task.CompletedDate!.Value.Kind);
    }

    [Theory]
    [InlineData(Status.ToDo)]
    [InlineData(Status.InProgress)]
    [InlineData(Status.Canceled)]
    public void ChangeTaskStatus_LeavingCompleted_ClearsCompletedDate(Status status)
    {
        var board = TestData.NewBoard();
        var task = board.AddTask("A", null, null, null, null);
        board.ChangeTaskStatus(task.TaskId, Status.Completed);

        board.ChangeTaskStatus(task.TaskId, status);

        Assert.Null(task.CompletedDate);
    }

    [Fact]
    public void ChangeTaskStatus_ToTheSameStatus_ChangesNothingOnTheTask()
    {
        var board = TestData.NewBoard();
        var task = board.AddTask("A", null, null, null, null);
        board.ChangeTaskStatus(task.TaskId, Status.Completed);
        var completedAt = task.CompletedDate;
        var modifiedAt = task.ModificationDate;
        TestClock.Tick();

        board.ChangeTaskStatus(task.TaskId, Status.Completed);

        Assert.Equal(completedAt, task.CompletedDate);
        Assert.Equal(modifiedAt, task.ModificationDate);
    }

    [Fact]
    public void ChangeTaskStatus_WithUnknownTask_Throws()
        => Assert.Throws<TaskNotFoundException>(
            () => TestData.NewBoard().ChangeTaskStatus(Guid.NewGuid(), Status.Completed));
}