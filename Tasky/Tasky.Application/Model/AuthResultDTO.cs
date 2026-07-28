namespace Tasky.Application.Model
{
    public class AuthResultDTO
    {
        public required string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
