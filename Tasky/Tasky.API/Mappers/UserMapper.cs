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

        public static CreateUserDTO Map(CreateUserRequest viewModel)
        {
            return new CreateUserDTO
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                Password = viewModel.Password
            };
        }

        public static LoginUserDTO Map(LoginUserRequest request)
        {
            return new LoginUserDTO
            {
                Login = request.Login,
                Password = request.Password,
            };
        }

        public static AuthResponse Map(AuthResultDTO result)
        {
            return new AuthResponse
            {
                AccessToken = result.AccessToken,
                Expiration = result.Expiration
            };
        }
    }
}
