using Tasky.Domain.Enums;
using Tasky.Domain.Exceptions;

namespace Tasky.Domain.Entities;

public class BoardEntity
{
    public Guid BoardId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public BoardType Type { get; private set; }

    public Guid OwnerId { get; private set; }
    public UserEntity Owner { get; private set; } = null!;

    private readonly List<TaskEntity> _tasks = new();
    public IReadOnlyCollection<TaskEntity> Tasks => _tasks;

    public DateTime CreationDate { get; private set; }
    public DateTime ModificationDate { get; private set; }

    private BoardEntity() { }

    public static BoardEntity Create(Guid ownerId, string name, string? description, BoardType type)
    {
        if (ownerId == Guid.Empty)
            throw new InvalidBoardException("Board owner is required.");

        var now = DateTime.UtcNow;

        return new BoardEntity
        {
            BoardId = Guid.NewGuid(),
            OwnerId = ownerId,
            Name = ValidateName(name),
            Description = description,
            Type = type,
            CreationDate = now,
            ModificationDate = now
        };
    }

    public void Update(string name, string? description)
    {
        Name = ValidateName(name);
        Description = description;
        UpdateModificationDate();
    }

    public void Rename(string newname)
    {
        Name = ValidateName(newname);
        UpdateModificationDate();
    }

    public void ChangeDescription(string? description)
    {
        Description = description;
        UpdateModificationDate();
    }

    public TaskEntity GetTask(Guid taskId)
    {
        return _tasks.FirstOrDefault(t => t.TaskId == taskId) ?? throw new TaskNotFoundException(taskId);
    }

    public TaskEntity AddTask(string title, string? description, Guid? responsibleId,
     int? estimate, DateTime? dueDate)
    {
        var position = _tasks.Count == 0 ? 0 : _tasks.Max(t => t.Position) + 1;

        var task = TaskEntity.Create(BoardId, title, description, responsibleId, estimate, dueDate, position);
        _tasks.Add(task);
        UpdateModificationDate();

        return task;
    }

    public TaskEntity ChangeTaskStatus(Guid taskId, Status status)
    {
        var task = GetTask(taskId);

        EnsureStatusAllowed(status);

        task.ChangeStatus(status);
        UpdateModificationDate();
        return task;
    }

    public void RemoveTask(Guid taskId)
    {
        _tasks.Remove(GetTask(taskId));
        UpdateModificationDate();
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidBoardException("Board name is required.");

        name = name.Trim();

        return name;
    }

    private void EnsureStatusAllowed(Status status)
    {
        if (Type == BoardType.List && status is not (Status.ToDo or Status.Completed))
            throw new InvalidTaskException(
                $"Tasks on a List board can only be {Status.ToDo} or {Status.Completed}.");
    }

    private void UpdateModificationDate() => ModificationDate = DateTime.UtcNow;
}
