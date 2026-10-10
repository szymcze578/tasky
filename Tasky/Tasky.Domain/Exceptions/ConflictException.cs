namespace Tasky.Domain.Exceptions;

public abstract class ConflictException(string message) : DomainException(message);
