using Tasky.API.ViewModels;
using Tasky.API.ViewModels.Auth;
using Tasky.Application.Model;

namespace Tasky.API.Mappers
{
    public static class UserMapper
    {
        public static CreateUserDTO Map(CreateUserViewModel viewModel)
        {
            return new CreateUserDTO
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                Password = viewModel.Password,
                Street = viewModel.Street,
                City = viewModel.City,
                PostalCode = viewModel.PostalCode,
                Country = viewModel.Country
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
    }
}
