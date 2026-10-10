namespace Tasky.Domain.Exceptions;

public abstract class NotFoundException(string message) : DomainException(message);
