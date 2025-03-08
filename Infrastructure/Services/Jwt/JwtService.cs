using Application.Services;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services.Jwt
{
    /// <summary>
    /// Represents a service for generating JWT tokens.
    /// </summary>
    /// <param name="configuration">The interface for accessing application configuration.</param>
    /// <param name="userRolesProvider">The provider for user roles.</param>
    public class JwtService(
        IConfiguration configuration,
        IUserRolesProvider userRolesProvider)
        : IJwtService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUserRolesProvider _userRolesProvider = userRolesProvider;

        /// <inheritdoc />
        /// <exception cref="KeyNotFoundException"></exception>
        public string GenerateToken(User user)
        {
            string roles = string.Join(',', _userRolesProvider.GetUserRoles(user.Roles));
            Claim[] claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, roles)
            ];

            SymmetricSecurityKey key = new(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Token") ?? throw new KeyNotFoundException("Token not found.")));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);
            DateTime issueTime = DateTime.UtcNow;

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = issueTime,
                Expires = issueTime.AddHours(1),
                SigningCredentials = creds,
                Issuer = _configuration.GetValue<string>("AppSettings:Issuer"),
                Audience = _configuration.GetValue<string>("AppSettings:Audience"),
            };

            JwtSecurityTokenHandler handler = new();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        /// <inheritdoc />
        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
