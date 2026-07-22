using System;
using System.Collections.Generic;
using System.Text;
using Tasky.Application.Model;

namespace Tasky.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Guid> Register(CreateUserDTO user);

        Task<string> Login(LoginUserDTO loginRequest);
    }
}
