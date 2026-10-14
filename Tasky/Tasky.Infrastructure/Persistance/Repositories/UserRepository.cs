using Tasky.Application.Exceptions;
using Tasky.Domain.Entities;
using Tasky.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Tasky.Application.Users;

namespace Tasky.Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{

    private readonly BoardDbContext dbContext;

    public UserRepository(BoardDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        var normalizedEmail = EmailHelper.Normalize(email);

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
            throw new DuplicateEmailException(user.Email);
        }
    }

    public async Task<UserEntity?> GetUserById(Guid userId)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task UpdateUser(UserEntity user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }

    public Task SaveChangesAsync(CancellationToken ct)
        => dbContext.SaveChangesAsync(ct);

    private static bool IsUniqueViolation(DbUpdateException ex)
        => ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation };

    public Task<UserEntity?> GetUserByEmail(string email)
    {
        var normalizedEmail = EmailHelper.Normalize(email);

        return dbContext.Users.FirstOrDefaultAsync(u => u.Email == normalizedEmail);
    }
}
