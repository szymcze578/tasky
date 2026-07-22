using Tasky.Application.Exceptions;
using Tasky.Application.Interfaces.Infrastructure;
using Tasky.Domain.Entities;
using Tasky.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
            var normalizedEmail = Email.Normalize(email);

            return await dbContext.Users.AnyAsync(u => u.Email == normalizedEmail);
        }

        public async Task<bool> ExistsById(Guid userId)
        {
            return await dbContext.Users.AnyAsync(u => u.UserId == userId);
        }

        public async Task CreateUser(UserEntity user)
        {
            dbContext.Users.Add(user);

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (IsUniqueViolation(ex))
            {
                throw new DuplicateEmailException("User with specified email already exists", ex);
            }
        }

        private static bool IsUniqueViolation(DbUpdateException ex)
            => ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation };
    }
}
