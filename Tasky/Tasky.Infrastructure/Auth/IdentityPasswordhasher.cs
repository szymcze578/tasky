using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tasky.Application.Common.Abstractions;

namespace Tasky.Infrastructure.Auth;

public class IdentityPasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<object> hasher = new();

    public string Hash(string password) => hasher.HashPassword(null!, password);

    public bool Verify(string password, string hash)
        => hasher.VerifyHashedPassword(null!, hash, password) != PasswordVerificationResult.Failed;
}
