namespace Tasky.Domain.Exceptions;

public class InvalidBoardException(string message) : DomainException(message);
