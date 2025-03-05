﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Domain.Entities.Identity;
using System.Security.Claims;
using System.Text;

namespace MindSpace.Infrastructure.Services.AuthenticationServices
{
    public class AccessTokenProvider(IConfiguration configuration) : IAccessTokenProvider
    {
        public string CreateToken(ApplicationUser user, string role)
        {
            var jwtSettings = configuration.GetSection("JwtAccessTokenSettings");
            string secretKey = jwtSettings["Secret"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, role),
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JwtAccessTokenSettings:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = jwtSettings["Issuer"]!,
                Audience = jwtSettings["Audience"]
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptior);

            return token;
        }
    }
}