using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.ProductService.Tests.Integration
{
    public static class JwtTokenHelper
    {
        private const string SecretKey = "ThisIsAVeryLongSecretKeyForJwtTokenGeneration123!";
        private const string Issuer = "InnoShopUserService";
        private const string Audience = "InnoShopUsers";

        public static string GenerateToken(Guid userId)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static HttpClient WithAuth(this HttpClient client, Guid userId)
        {
            var generateToken= GenerateToken(userId);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", generateToken);
            return client;
        }
    }
}
