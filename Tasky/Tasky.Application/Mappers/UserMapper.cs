using Tasky.Application.Model;
using Tasky.Domain.Entities;

namespace Tasky.Application.Mappers
{
    public static class UserMapper
    {

        public static UserEntity Map(CreateUserDTO userDTO)
        {
            return new UserEntity
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                Password = userDTO.Password,
                Address = new Address 
                {   Street = userDTO.Street,
                    City = userDTO.City,
                    PostalCode = userDTO.PostalCode,
                    Country = userDTO.Country,             
                }
            };
        }
    }
}
