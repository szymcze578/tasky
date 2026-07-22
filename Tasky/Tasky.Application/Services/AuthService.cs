using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tasky.Application.Exceptions;
using Tasky.Application.Interfaces.Infrastructure;
using Tasky.Application.Interfaces.Services;
using Tasky.Application.Mappers;
using Tasky.Application.Model;
using Tasky.Domain.Entities;

namespace Tasky.Application.Services
{
    public class AuthService(IUserRepository userRepository, IPasswordHasher<UserEntity> passwordHasher) : IAuthService
    {
        public async Task<Guid> Register(CreateUserDTO user)
        {
            ArgumentNullException.ThrowIfNull(user, "Created user cannot be empty!");

            var normalizedEmail = Email.Normalize(user.Email);

            if (await userRepository.ExistsByEmail(normalizedEmail))
            {
                throw new DuplicateEmailException("User with specified email already exists");
            }

            var newUser = CreateUserEntity(user, normalizedEmail);

            await userRepository.CreateUser(newUser);

            return newUser.UserId;
        }

        public Task<string> Login(LoginUserDTO loginRequest)
        {
            throw new NotImplementedException();
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
}
