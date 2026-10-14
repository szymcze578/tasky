namespace Tasky.Application.Common.Abstractions;

public interface ICurrentUser
{
    Guid UserId { get; }
}
