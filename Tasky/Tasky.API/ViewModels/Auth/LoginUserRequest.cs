using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tasky.API.ViewModels.Auth
{
    public class LoginUserRequest
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
