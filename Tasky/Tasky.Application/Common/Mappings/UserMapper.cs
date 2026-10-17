using Tasky.Application.Users.Dtos;
using Tasky.Domain.Entities;

namespace Tasky.Application.Common.Mappings;

public static class UserMapper
{
    public static UserDTO Map(UserEntity userEntity)
    {
        return new UserDTO
        {
            UserId = userEntity.UserId,
            UserName = userEntity.UserName,
            Email = userEntity.Email,
            IsActive = userEntity.IsActive,
            Plan = "Free plan"
        };
    }
}
