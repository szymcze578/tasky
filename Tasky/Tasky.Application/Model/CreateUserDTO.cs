using System;
using System.Collections.Generic;
using System.Text;

namespace Tasky.Application.Model
{
    public class CreateUserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
