using Tasky.Application.Interfaces.Infrastructure;
using Tasky.Domain.Entities;
using Tasky.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tasky.Infrastructure.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly BoardDbContext dbContext;

        public UserRepository(BoardDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsById(Guid userId)
        {
            return await dbContext.Users.AnyAsync(u => u.UserId == userId);
        }

        public async Task CreateUser(UserEntity user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
