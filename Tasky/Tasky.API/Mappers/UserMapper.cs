using Tasky.API.ViewModels;
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

        public static CreateUserViewModel Map(CreateUserDTO dto)
        {
            return new CreateUserViewModel
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = dto.Password,
                Street = dto.Street,
                City = dto.City,
                PostalCode = dto.PostalCode,
                Country = dto.Country
            };
        }
    }
}
