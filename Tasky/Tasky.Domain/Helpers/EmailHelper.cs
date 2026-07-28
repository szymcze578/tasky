namespace Tasky.Domain.Helpers
{
    /// <summary>
    /// Canonical form for email addresses. Every email must be normalized through
    /// <see cref="Normalize"/> before it is stored or used as a lookup key, otherwise
    /// the unique index on UserEntity.Email will not catch case-only duplicates.
    /// </summary>
    public static class EmailHelper
    {
        public static string Normalize(string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            return email.Trim().ToLowerInvariant();
        }
    }
}
