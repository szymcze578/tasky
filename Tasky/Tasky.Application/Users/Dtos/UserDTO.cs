using System;
using System.Collections.Generic;
using System.Text;

namespace Tasky.Application.Users.Dtos
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Plan { get; set; }
    }
}
