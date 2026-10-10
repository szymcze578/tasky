using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tasky.Application.Common.Abstractions;
using Tasky.Application.Common.Mappings;
using Tasky.Application.Exceptions;
using Tasky.Application.Users.Dtos;
using Tasky.Domain.Entities;
using Tasky.Domain.Helpers;

namespace Tasky.Application.Users;

public class AuthService(IUserRepository userRepository, IPasswordHasher<UserEntity> passwordHasher, ITokenGenerator tokenGenerator) : IAuthService
{
    public async Task<Guid> Register(CreateUserDTO user)
    {
        ArgumentNullException.ThrowIfNull(user, "Created user cannot be empty!");

        var normalizedEmail = EmailHelper.Normalize(user.Email);

        if (await userRepository.ExistsByEmail(normalizedEmail))
        {
            throw new DuplicateEmailException("User with specified email already exists");
        }

        var newUser = CreateUserEntity(user, normalizedEmail);

        await userRepository.CreateUser(newUser);

        return newUser.UserId;
    }

    public async Task<AuthResultDTO> Login(LoginUserDTO loginRequest)
    {
        ArgumentNullException.ThrowIfNull(loginRequest);

        var normalizedEmail = EmailHelper.Normalize(loginRequest.Login);

        var user = await userRepository.GetUserByEmail(normalizedEmail);

        if (user == null)
        {
            throw new UserNotFoundException("Failed to log in!");
        }

        var result = passwordHasher.VerifyHashedPassword(user, user.Password, loginRequest.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new InvalidCredentialsException("Failed to log in!");
        }

        return tokenGenerator.GenerateToken(user);
    }

    private UserEntity CreateUserEntity(CreateUserDTO user, string normalizedEmail)
    {
        var hashedPassword = passwordHasher.HashPassword(null!, user.Password);

        var newUser = UserMapper.Map(user);
        newUser.UserId = Guid.NewGuid();
        newUser.Email = normalizedEmail;
        newUser.Password = hashedPassword;
        newUser.CreationDate = DateTime.UtcNow;
        newUser.ModificationDate = newUser.CreationDate;
        newUser.IsActive = true;

        return newUser;
    }
}
