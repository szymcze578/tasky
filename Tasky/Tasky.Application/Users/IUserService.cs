using Tasky.Application.Users.Dtos;

namespace Tasky.Application.Users;

public interface IUserService
{
    Task<UserDto> GetMe(CancellationToken ct);
    Task<UserDto> UpdateMe(UpdateUserRequest request, CancellationToken ct);
    Task<UserDto> SetAddress(AddressRequest request, CancellationToken ct);
    Task RemoveAddress(CancellationToken ct);
}
