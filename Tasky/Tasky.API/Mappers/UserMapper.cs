using Tasky.API.ViewModels;
using Tasky.API.ViewModels.Auth;
using Tasky.Application.Users.Dtos;

namespace Tasky.API.Mappers
{
    public static class UserMapper
    {
        public static UserViewModel Map(UserDTO userDTO)
        {
            return new UserViewModel
            {
                UserId = userDTO.UserId,
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                IsActive = userDTO.IsActive,
                Plan = userDTO.Plan
            };
        }
    }
}
