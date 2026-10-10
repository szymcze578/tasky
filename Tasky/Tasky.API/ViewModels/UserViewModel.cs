using System;

namespace Tasky.API.ViewModels;

public class UserViewModel
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public string Plan { get; set; }
}
