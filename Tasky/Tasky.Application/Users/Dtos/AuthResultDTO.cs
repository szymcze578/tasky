namespace Tasky.Application.Users.Dtos
{
    public class AuthResultDTO
    {
        public required string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
