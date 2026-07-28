using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tasky.Application.Interfaces.Infrastructure;
using Tasky.Application.Model;
using Tasky.Domain.Entities;

namespace Tasky.Infrastructure.Implementation
{
    public class JwtTokenGenerator(IConfiguration config) : ITokenGenerator
    {

        public AuthResultDTO GenerateToken(UserEntity user)
        {
            var key = config["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(config.GetValue("Jwt:ExpiryMinutes", 60));

            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Email", user.Email),
                new Claim("UserName", user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: credentials);

            return new AuthResultDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expires
            };
        }
    }
}
