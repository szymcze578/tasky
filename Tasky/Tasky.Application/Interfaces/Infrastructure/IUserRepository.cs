using Tasky.Domain.Entities;

namespace Tasky.Application.Interfaces.Infrastructure
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmail(string email);
        Task<bool> ExistsById(Guid userId);
        Task CreateUser(UserEntity user);
    }
}
