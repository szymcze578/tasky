using Tasky.Domain.Entities;
using Tasky.Domain.Exceptions;

namespace Tasky.Domain.Tests;

public class TaskEntityTests
{
    private static TaskEntity NewTask()
        => TestData.NewBoard().AddTask("Setup CI", "Opis", null, 3, null);

    [Fact]
    public void Update_ChangesAllFields_AndTouchesModificationDate()
    {
        var task = NewTask();
        var before = task.ModificationDate;
        var dueDate = DateTime.UtcNow.AddDays(7);
        TestClock.Tick();

        task.Update("  Nowy tytuł  ", "Nowy opis", 5, dueDate);

        Assert.Equal("Nowy tytuł", task.Title);
        Assert.Equal("Nowy opis", task.Description);
        Assert.Equal(5, task.Estimate);
        Assert.Equal(dueDate, task.DueDate);
        Assert.True(task.ModificationDate > before);
    }

    [Fact]
    public void Update_WithNullValues_ClearsOptionalFields()
    {
        var task = NewTask();

        task.Update("Setup CI", null, null, null);

        Assert.Null(task.Description);
        Assert.Null(task.Estimate);
        Assert.Null(task.DueDate);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Update_WithBlankTitle_ThrowsAndChangesNothing(string? title)
    {
        var task = NewTask();

        Assert.Throws<InvalidTaskException>(() => task.Update(title!, "Nowy opis", 5, null));

        Assert.Equal("Setup CI", task.Title);
        Assert.Equal("Opis", task.Description);
        Assert.Equal(3, task.Estimate);
    }

    [Fact]
    public void Update_WithNegativeEstimate_ThrowsAndChangesNothing()
    {
        var task = NewTask();

        Assert.Throws<InvalidTaskException>(() => task.Update("Nowy tytuł", "Nowy opis", -1, null));

        Assert.Equal("Setup CI", task.Title);
        Assert.Equal("Opis", task.Description);
        Assert.Equal(3, task.Estimate);
    }
}