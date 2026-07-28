using Tasky.Domain.Entities;

namespace Tasky.Application.Interfaces.Infrastructure
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmail(string email);
        Task<bool> ExistsById(Guid userId);
        Task<UserEntity?> GetUserById(Guid userId);
        Task<UserEntity?> GetUserByEmail(string email);
        Task CreateUser(UserEntity user);
        Task UpdateUser(UserEntity user);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
