namespace Tasky.API.ViewModels.Auth
{
    public class AuthResponse
    {
        public required string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
