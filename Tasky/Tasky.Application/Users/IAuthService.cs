using Tasky.Application.Users.Dtos;

namespace Tasky.Application.Users;

public interface IAuthService
{
    Task<Guid> Register(CreateUserRequest request, CancellationToken ct);
    Task<AuthResponse> Login(LoginUserRequest request, CancellationToken ct);
}
