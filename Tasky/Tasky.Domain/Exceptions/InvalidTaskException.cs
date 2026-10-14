namespace Tasky.Domain.Exceptions;

public class InvalidTaskException(string message) : DomainException(message);

