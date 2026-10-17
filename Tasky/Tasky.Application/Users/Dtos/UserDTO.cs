namespace Tasky.Application.Users.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public AddressDto? Address { get; set; }
    public DateTime CreationDate { get; set; }
    public required string Plan { get; set; }
}
