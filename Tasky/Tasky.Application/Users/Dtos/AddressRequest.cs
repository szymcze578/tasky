using System.ComponentModel.DataAnnotations;
using Tasky.Domain.Entities;

namespace Tasky.Application.Users.Dtos;

public class AddressRequest
{
    [Required, StringLength(Address.StreetMaxLength)]
    public required string Street { get; set; }

    [Required, StringLength(Address.CityMaxLength)]
    public required string City { get; set; }

    [Required, StringLength(Address.PostalCodeMaxLength)]
    public required string PostalCode { get; set; }

    [Required, StringLength(Address.CountryMaxLength)]
    public required string Country { get; set; }
}
