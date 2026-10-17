using Tasky.Domain.Entities;

namespace Tasky.Application.Users
{
    public interface IUserRepository
    {
        Task Add(UserEntity user, CancellationToken ct);
        Task<UserEntity?> GetByEmail(string email, CancellationToken ct);
        Task<bool> ExistsByEmail(string email);
        Task<bool> ExistsById(Guid userId);
        Task<UserEntity?> GetById(Guid userId, CancellationToken ct);
        Task SaveChanges(CancellationToken ct);
    }
}
