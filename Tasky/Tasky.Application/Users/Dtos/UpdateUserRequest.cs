using System.ComponentModel.DataAnnotations;
using Tasky.Domain.Entities;

namespace Tasky.Application.Users.Dtos;

public class UpdateUserRequest
{
    [Required, StringLength(UserEntity.UserNameMaxLength, MinimumLength = UserEntity.UserNameMinLength)]
    public required string UserName { get; set; }
}
