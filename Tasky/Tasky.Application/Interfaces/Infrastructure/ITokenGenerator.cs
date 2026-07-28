using System;
using System.Collections.Generic;
using System.Text;
using Tasky.Application.Model;
using Tasky.Domain.Entities;

namespace Tasky.Application.Interfaces.Infrastructure
{
    public interface ITokenGenerator
    {
        AuthResultDTO GenerateToken(UserEntity user);
    }
}
