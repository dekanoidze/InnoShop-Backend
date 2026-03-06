using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoShop.UserService.Application.Services;
using InnoShop.UserService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InnoShop.UserService.Infrastructure.Services
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        public string GenerateToken(User user)
        {
            var SecretKey = configuration["JwtSettings:SecretKey"];
            var Issuer = configuration["JwtSettings:Issuer"];
            var Audience = configuration["JwtSettings:Audience"];
            var Expiration = int.Parse(configuration["JwtSettings:ExpirationMinutes"]!);

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey!));
            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role),
            };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: Claims,
                expires: DateTime.UtcNow.AddMinutes(Expiration),
                signingCredentials: Credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    
    }
}
