using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tasky.Domain.Entities;
using Tasky.Application.Exceptions;
using Tasky.Application.Common.Mappings;
using Tasky.Application.Users.Dtos;

namespace Tasky.Application.Users
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<UserDTO?> GetUser(Guid userId)
        {
            var user = await userRepository.GetUserById(userId);
            return UserMapper.Map(user);
        }

        public async Task SetAddress(Guid userId, AddressDTO address)
        {
            var user = await userRepository.GetUserById(userId);

            if (user == null) {
                throw new UserNotFoundException($"User with id {userId} does not exist.");
            }

            user.Address = new Address
            {
                Street = address.Street,
                City = address.City,
                PostalCode = address.PostalCode,
                Country = address.Country
            };

            user.ModificationDate = DateTime.UtcNow;

            await userRepository.SaveChangesAsync();
        }
    }
}
