using Tasky.Domain.Exceptions;

namespace Tasky.Application.Exceptions;

public class DuplicateEmailException(string email)
 : ConflictException($"Email '{email}' is already in use.")
{
    public string Email { get; } = email;
}
