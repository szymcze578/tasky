using System;
using System.Collections.Generic;
using System.Text;
using Tasky.Application.Users.Dtos;

namespace Tasky.Application.Users
{
    public interface IAuthService
    {
        Task<Guid> Register(CreateUserDTO user);

        Task<AuthResultDTO> Login(LoginUserDTO loginRequest);
    }
}
