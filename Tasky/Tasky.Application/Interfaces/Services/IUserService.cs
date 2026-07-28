using Tasky.Application.Model;
namespace Tasky.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task SetAddress(Guid userId, AddressDTO address);
    }
}
