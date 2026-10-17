using Tasky.Domain.Entities;
using Tasky.Application.Exceptions;
using Tasky.Application.Common.Mappings;
using Tasky.Application.Users.Dtos;
using Tasky.Application.Common.Abstractions;

namespace Tasky.Application.Users;

public class UserService(IUserRepository users, ICurrentUser currentUser) : IUserService
{
    public async Task<UserDto> SetAddress(AddressRequest request, CancellationToken ct)
    {
        var user = await GetCurrentUser(ct);

        user.UpdateAddress(new Address(
            request.Street, request.City, request.PostalCode, request.Country));

        await users.SaveChanges(ct);
        return UserMapper.Map(user);
    }

    public async Task<UserDto> GetMe(CancellationToken ct)
    {
        var me = await GetCurrentUser(ct);
        return UserMapper.Map(me);
    }


    public async Task<UserDto> UpdateMe(UpdateUserRequest request, CancellationToken ct)
    {
        var user = await GetCurrentUser(ct);

        user.ChangeUserName(request.UserName);

        await users.SaveChanges(ct);
        return UserMapper.Map(user);
    }

    public async Task RemoveAddress(CancellationToken ct)
    {
        var user = await GetCurrentUser(ct);

        user.UpdateAddress(null);

        await users.SaveChanges(ct);
    }

    private async Task<UserEntity> GetCurrentUser(CancellationToken ct)
    => await users.GetById(currentUser.UserId, ct)
       ?? throw new UserNotFoundException(currentUser.UserId.ToString());
}
