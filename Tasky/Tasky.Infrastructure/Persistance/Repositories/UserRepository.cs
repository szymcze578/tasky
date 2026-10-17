using Tasky.Application.Exceptions;
using Tasky.Domain.Entities;
using Tasky.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Tasky.Application.Users;

namespace Tasky.Infrastructure.Persistance.Repositories;

public class UserRepository(BoardDbContext dbContext) : IUserRepository
{
    public async Task Add(UserEntity user, CancellationToken ct)
    {
        await dbContext.Users.AddAsync(user, ct);

        try
        {
            await dbContext.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation })
        {
            throw new DuplicateEmailException(user.Email);
        }
    }

    public async Task<UserEntity?> GetByEmail(string email, CancellationToken ct)
    {
        return await dbContext.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Email == email, ct);
    }

    public async Task<UserEntity?> GetUserById(Guid userId)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
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

    public async Task UpdateUser(UserEntity user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }

    public Task SaveChangesAsync(CancellationToken ct)
        => dbContext.SaveChangesAsync(ct);

    private static bool IsUniqueViolation(DbUpdateException ex)
        => ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation };


}
