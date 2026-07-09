using Tasky.Application.Interfaces.Infrastructure;
using Tasky.Application.Interfaces.Services;
using Tasky.Application.Mappers;
using Tasky.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tasky.Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<Guid> CreateUser(CreateUserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Created user cannot be empty!");
            }

            if (await userRepository.ExistsByEmail(user.Email))
            {
                throw new InvalidOperationException("User with specified email already exists");
            }

            try
            {
                var newUser = UserMapper.Map(user);

                newUser.UserId = Guid.NewGuid();
                newUser.CreationDate = DateTime.UtcNow;
                newUser.IsActive = true;

                await userRepository.CreateUser(newUser);
                return newUser.UserId;
            }
            catch {
                throw new Exception("Failed to create user");
            }
        }
    }
}
