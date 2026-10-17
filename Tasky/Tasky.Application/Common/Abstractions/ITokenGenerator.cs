using System;
using System.Collections.Generic;
using System.Text;
using Tasky.Application.Users.Dtos;
using Tasky.Domain.Entities;

namespace Tasky.Application.Common.Abstractions;

public interface ITokenGenerator
{
    AuthResponse GenerateToken(UserEntity user);
}
