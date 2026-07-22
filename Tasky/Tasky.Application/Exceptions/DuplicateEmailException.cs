namespace Tasky.Application.Exceptions
{
    /// <summary>
    /// Thrown when a user could not be persisted because its email is already taken.
    /// Infrastructure translates the database unique-constraint violation into this
    /// exception so the application layer does not need to know about EF Core or Npgsql.
    /// </summary>
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(string message) : base(message) { }

        public DuplicateEmailException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
