using Tasky.Domain.Exceptions;

namespace Tasky.Domain.Entities;

public class Address
{
    public const int StreetMaxLength = 200;
    public const int CityMaxLength = 100;
    public const int PostalCodeMaxLength = 20;
    public const int CountryMaxLength = 100;

    public Address(string street, string city, string postalCode, string country)
    {
        Street = Require(street, nameof(Street), StreetMaxLength);
        City = Require(city, nameof(City), CityMaxLength);
        PostalCode = Require(postalCode, nameof(PostalCode), PostalCodeMaxLength);
        Country = Require(country, nameof(Country), CountryMaxLength);
    }

    public string Street { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string Country { get; private set; }

    private static string Require(string? value, string field, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAddressException($"{field} is required.");

        value = value.Trim();

        if (value.Length > maxLength)
            throw new InvalidAddressException($"{field} cannot exceed {maxLength} characters.");

        return value;
    }
}
