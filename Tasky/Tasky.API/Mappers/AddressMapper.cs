using Tasky.API.ViewModels.Auth;
using Tasky.Application.Model;

namespace Tasky.API.Mappers
{
    public static class AddressMapper
    {
        public static AddressDTO Map(AddressViewModel viewModel)
        {
            return new AddressDTO
            {
                Street = viewModel.Street,
                City = viewModel.City,
                PostalCode = viewModel.PostalCode,
                Country = viewModel.Country
            };
        }
    }
}
