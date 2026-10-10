using Tasky.Application.Users.Dtos;
namespace Tasky.Application.Users
{
    public interface IUserService
    {
        Task<UserDTO?> GetUser(Guid userId);
        Task SetAddress(Guid userId, AddressDTO address);
    }
}
