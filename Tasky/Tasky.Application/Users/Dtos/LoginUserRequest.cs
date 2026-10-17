using System.ComponentModel.DataAnnotations;

namespace Tasky.Application.Users.Dtos;

public class LoginUserRequest
{
    [Required]
    public required string Login { get; set; } // email

    [Required, StringLength(128)]
    public required string Password { get; set; }
}
