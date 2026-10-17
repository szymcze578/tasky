namespace Tasky.Application.Users.Dtos;

public record AuthResponse(
    string AccessToken,
    DateTime ExpiresAt,            // zawsze UTC
    string TokenType = "Bearer");
