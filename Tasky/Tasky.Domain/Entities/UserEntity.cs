using System.Net.Mail;
using Tasky.Domain.Exceptions;

namespace Tasky.Domain.Entities;

public class UserEntity
{
    public const int UserNameMinLength = 3;
    public const int UserNameMaxLength = 50;
    public const int EmailMaxLength = 256;

    public Guid UserId { get; private set; }
    public string UserName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public Address? Address { get; private set; }
    public bool IsActive { get; private set; }

    public DateTime CreationDate { get; private set; }
    public DateTime ModificationDate{ get; private set; }

    private UserEntity() { }

    public static UserEntity Create(string userName, string email, string passwordHash, Address? address = null)
    {
        var now = DateTime.UtcNow;

        return new UserEntity
        {
            UserId = Guid.NewGuid(),
            UserName = ValidateUserName(userName),
            Email = NormalizeEmail(email),
            PasswordHash = ValidatePasswordHash(passwordHash),
            Address = address,
            IsActive = true,
            CreationDate = now,
            ModificationDate = now
        };
    }

    public void ChangeUserName(string userName)
    {
        UserName = ValidateUserName(userName);
        UpdateModificationDate();
    }

    public void ChangeEmail(string email)
    {
        Email = NormalizeEmail(email);
        UpdateModificationDate();
    }

    public void ChangePasswordHash(string passwordHash)
    {
        PasswordHash = ValidatePasswordHash(passwordHash);
        UpdateModificationDate();
    }

    public void UpdateAddress(Address? address)
    {
        Address = address;
        UpdateModificationDate();
    }

    public void Deactivate()
    {
        if (!IsActive) return;
        IsActive = false;
        UpdateModificationDate();
    }

    public void Activate()
    {
        if (IsActive) return;
        IsActive = true;
        UpdateModificationDate();
    }

    private static string ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new InvalidUserException("User name is required.");

        userName = userName.Trim();

        if (userName.Length is < UserNameMinLength or > UserNameMaxLength)
            throw new InvalidUserException(
                $"User name must be between {UserNameMinLength} and {UserNameMaxLength} characters.");

        return userName;
    }

    private static string NormalizeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidUserException("Email is required.");

        email = email.Trim().ToLowerInvariant();

        if (email.Length > EmailMaxLength
            || !MailAddress.TryCreate(email, out var parsed)
            || parsed.Address != email)
            throw new InvalidUserException("Email address is invalid.");

        return email;
    }

    private static string ValidatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new InvalidUserException("Password hash is required.");

        return passwordHash;
    }

    private void UpdateModificationDate() => ModificationDate = DateTime.UtcNow;
}