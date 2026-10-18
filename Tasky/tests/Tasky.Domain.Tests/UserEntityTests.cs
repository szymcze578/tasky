using Tasky.Domain.Entities;
using Tasky.Domain.Exceptions;

namespace Tasky.Domain.Tests;

public class UserEntityTests
{
    // --- Create ---

    [Fact]
    public void Create_WithValidData_SetsProperties()
    {
        var address = new Address("Marszałkowska 1", "Warszawa", "00-001", "Polska");

        var user = UserEntity.Create("  jan.kowalski  ", "  Jan@Example.COM  ", "hash", address);

        Assert.NotEqual(Guid.Empty, user.UserId);
        Assert.Equal("jan.kowalski", user.UserName);
        Assert.Equal("jan@example.com", user.Email);
        Assert.Equal("hash", user.PasswordHash);
        Assert.Same(address, user.Address);
        Assert.True(user.IsActive);
        Assert.Equal(user.CreationDate, user.ModificationDate);
    }

    [Fact]
    public void Create_WithoutAddress_LeavesAddressNull()
        => Assert.Null(TestData.NewUser().Address);

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("ab")]
    [InlineData("  ab  ")]
    public void Create_WithInvalidUserName_Throws(string? userName)
        => Assert.Throws<InvalidUserException>(
            () => UserEntity.Create(userName!, "jan@example.com", "hash"));

    [Theory]
    [InlineData(UserEntity.UserNameMinLength)]
    [InlineData(UserEntity.UserNameMaxLength)]
    public void Create_AcceptsUserNameLengthBoundaries(int length)
    {
        var user = UserEntity.Create(new string('a', length), "jan@example.com", "hash");

        Assert.Equal(length, user.UserName.Length);
    }

    [Fact]
    public void Create_WithTooLongUserName_Throws()
        => Assert.Throws<InvalidUserException>(
            () => UserEntity.Create(new string('a', UserEntity.UserNameMaxLength + 1), "jan@example.com", "hash"));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("plainaddress")]
    [InlineData("@example.com")]
    [InlineData("Jan Kowalski <jan@example.com>")]
    public void Create_WithInvalidEmail_Throws(string? email)
        => Assert.Throws<InvalidUserException>(
            () => UserEntity.Create("jan.kowalski", email!, "hash"));

    [Fact]
    public void Create_WithTooLongEmail_Throws()
    {
        var email = new string('a', UserEntity.EmailMaxLength) + "@example.com";

        Assert.Throws<InvalidUserException>(() => UserEntity.Create("jan.kowalski", email, "hash"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithBlankPasswordHash_Throws(string? hash)
        => Assert.Throws<InvalidUserException>(
            () => UserEntity.Create("jan.kowalski", "jan@example.com", hash!));

    // --- zmiany ---

    [Fact]
    public void ChangeUserName_TrimsAndTouches()
    {
        var user = TestData.NewUser();
        var before = user.ModificationDate;
        TestClock.Tick();

        user.ChangeUserName("  nowa.nazwa  ");

        Assert.Equal("nowa.nazwa", user.UserName);
        Assert.True(user.ModificationDate > before);
    }

    [Fact]
    public void ChangeUserName_WithInvalidName_ThrowsAndChangesNothing()
    {
        var user = TestData.NewUser();

        Assert.Throws<InvalidUserException>(() => user.ChangeUserName("ab"));

        Assert.Equal("jan.kowalski", user.UserName);
    }

    [Fact]
    public void ChangeEmail_NormalizesEmail()
    {
        var user = TestData.NewUser();

        user.ChangeEmail("  Nowy@Example.com ");

        Assert.Equal("nowy@example.com", user.Email);
    }

    [Fact]
    public void ChangeEmail_WithInvalidEmail_ThrowsAndChangesNothing()
    {
        var user = TestData.NewUser();

        Assert.Throws<InvalidUserException>(() => user.ChangeEmail("not-an-email"));

        Assert.Equal("jan@example.com", user.Email);
    }

    [Fact]
    public void ChangePasswordHash_ReplacesHash_AndRejectsBlank()
    {
        var user = TestData.NewUser();

        user.ChangePasswordHash("new-hash");
        Assert.Equal("new-hash", user.PasswordHash);

        Assert.Throws<InvalidUserException>(() => user.ChangePasswordHash(" "));
        Assert.Equal("new-hash", user.PasswordHash);
    }

    [Fact]
    public void UpdateAddress_SetsAndClearsAddress()
    {
        var user = TestData.NewUser();
        var address = new Address("Marszałkowska 1", "Warszawa", "00-001", "Polska");

        user.UpdateAddress(address);
        Assert.Same(address, user.Address);

        user.UpdateAddress(null);
        Assert.Null(user.Address);
    }

    // --- aktywacja ---

    [Fact]
    public void Deactivate_MakesUserInactive_AndTouches()
    {
        var user = TestData.NewUser();
        var before = user.ModificationDate;
        TestClock.Tick();

        user.Deactivate();

        Assert.False(user.IsActive);
        Assert.True(user.ModificationDate > before);
    }

    [Fact]
    public void Deactivate_WhenAlreadyInactive_ChangesNothing()
    {
        var user = TestData.NewUser();
        user.Deactivate();
        var modifiedAt = user.ModificationDate;
        TestClock.Tick();

        user.Deactivate();

        Assert.Equal(modifiedAt, user.ModificationDate);
    }

    [Fact]
    public void Activate_RestoresInactiveUser_AndIsIdempotent()
    {
        var user = TestData.NewUser();
        user.Deactivate();

        user.Activate();
        Assert.True(user.IsActive);

        var modifiedAt = user.ModificationDate;
        TestClock.Tick();
        user.Activate();
        Assert.Equal(modifiedAt, user.ModificationDate);
    }
}