namespace Tasky.Domain.Exceptions;

public class InvalidAddressException(string message) : DomainException(message);
