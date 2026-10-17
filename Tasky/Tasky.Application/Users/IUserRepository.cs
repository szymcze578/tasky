using Tasky.Domain.Entities;

namespace Tasky.Application.Users
{
    public interface IUserRepository
    {
        Task Add(UserEntity user, CancellationToken ct);
        Task<UserEntity?> GetByEmail(string email, CancellationToken ct);
        Task<bool> ExistsByEmail(string email);
        Task<bool> ExistsById(Guid userId);
        Task<UserEntity?> GetUserById(Guid userId);
        Task UpdateUser(UserEntity user);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
