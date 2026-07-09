using Tasky.Application.Model;
namespace Tasky.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<Guid> CreateUser(CreateUserDTO user);
    }
}
