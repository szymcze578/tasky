using Tasky.Domain.Entities;
using Tasky.Domain.Exceptions;

namespace Tasky.Domain.Tests;

public class AddressTests
{
    private static Address Build(
        string? street = null, string? city = null, string? postalCode = null, string? country = null)
        => new(street ?? "Marszałkowska 1", city ?? "Warszawa", postalCode ?? "00-001", country ?? "Polska");

    [Fact]
    public void Constructor_WithValidData_SetsAndTrimsFields()
    {
        var address = new Address("  Marszałkowska 1 ", " Warszawa", "00-001 ", " Polska ");

        Assert.Equal("Marszałkowska 1", address.Street);
        Assert.Equal("Warszawa", address.City);
        Assert.Equal("00-001", address.PostalCode);
        Assert.Equal("Polska", address.Country);
    }

    [Theory]
    [InlineData("Street")]
    [InlineData("City")]
    [InlineData("PostalCode")]
    [InlineData("Country")]
    public void Constructor_WithBlankField_ThrowsNamingTheField(string field)
    {
        var ex = Assert.Throws<InvalidAddressException>(() => field switch
        {
            "Street" => Build(street: " "),
            "City" => Build(city: " "),
            "PostalCode" => Build(postalCode: " "),
            _ => Build(country: " ")
        });

        Assert.Contains(field, ex.Message);
    }

    [Theory]
    [InlineData("Street")]
    [InlineData("City")]
    [InlineData("PostalCode")]
    [InlineData("Country")]
    public void Constructor_WithTooLongField_Throws(string field)
        => Assert.Throws<InvalidAddressException>(() => field switch
        {
            "Street" => Build(street: new string('x', Address.StreetMaxLength + 1)),
            "City" => Build(city: new string('x', Address.CityMaxLength + 1)),
            "PostalCode" => Build(postalCode: new string('x', Address.PostalCodeMaxLength + 1)),
            _ => Build(country: new string('x', Address.CountryMaxLength + 1))
        });

    [Fact]
    public void Constructor_AcceptsFieldsAtMaxLength()
    {
        var address = Build(street: new string('x', Address.StreetMaxLength));

        Assert.Equal(Address.StreetMaxLength, address.Street.Length);
    }
}