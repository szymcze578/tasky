using Tasky.Application.Common.Abstractions;
using Tasky.Application.Exceptions;
using Tasky.Application.Users.Dtos;
using Tasky.Domain.Entities;
using Tasky.Domain.Helpers;

namespace Tasky.Application.Users;

public class AuthService(IUserRepository users, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator) : IAuthService
{
    public async Task<Guid> Register(CreateUserRequest request, CancellationToken ct)
    {
        var email = EmailNormalizer.Normalize(request.Email);

        if (await users.ExistsByEmail(email))
            throw new DuplicateEmailException(email);

        var user = UserEntity.Create(
            request.UserName, email, passwordHasher.Hash(request.Password));

        await users.Add(user, ct);

        return user.UserId;
    }

    public async Task<AuthResponse> Login(LoginUserRequest request, CancellationToken ct)
    {
        var email = EmailNormalizer.Normalize(request.Login);
        var user = await users.GetByEmail(email, ct);

        if (user is null
            || !user.IsActive
            || !passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new InvalidCredentialsException();

        return tokenGenerator.GenerateToken(user);
    }
}
