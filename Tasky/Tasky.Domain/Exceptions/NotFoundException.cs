namespace Tasky.Domain.Exceptions;

public abstract class NotFoundException(string message) : DomainException(message);

public class BoardNotFoundException(Guid boardId)
    : NotFoundException($"Board '{boardId}' was not found.")
{
    public Guid BoardId { get; } = boardId;
}

public class TaskNotFoundException(Guid taskId)
    : NotFoundException($"Task '{taskId}' was not found.")
{
    public Guid TaskId { get; } = taskId;
}

public class UserNotFoundException(Guid userId)
    : NotFoundException($"User '{userId}' was not found.")
{
    public Guid UserId { get; } = userId;
}
