using Tasky.Application.Users.Dtos;
using Tasky.Domain.Entities;

namespace Tasky.Application.Common.Mappings;

public static class UserMapper
{
    public static UserDto Map(UserEntity userEntity)
    {
        return new UserDto
        {
            Id = userEntity.UserId,
            UserName = userEntity.UserName,
            Email = userEntity.Email,
            Address = userEntity.Address is null ? null : Map(userEntity.Address),
            CreationDate = userEntity.CreationDate,
            Plan = "Free plan"
        };
    }

    public static AddressDto Map(Address address)
    {
        return new AddressDto
        {
            Street = address.Street,
            City = address.City,
            PostalCode = address.PostalCode,
            Country = address.Country,
        };
    }
}
