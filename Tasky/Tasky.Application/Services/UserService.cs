using Tasky.Application.Interfaces.Infrastructure;
using Tasky.Application.Interfaces.Services;
using Tasky.Application.Mappers;
using Tasky.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tasky.Domain.Entities;
using Tasky.Application.Exceptions;

namespace Tasky.Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
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
