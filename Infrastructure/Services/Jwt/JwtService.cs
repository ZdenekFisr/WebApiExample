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
    /// <param name="currentUtcTimeProvider">The provider to get the current UTC time.</param>
    public class JwtService(
        IConfiguration configuration,
        ICurrentUtcTimeProvider currentUtcTimeProvider)
        : IJwtService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider = currentUtcTimeProvider;

        /// <inheritdoc />
        /// <exception cref="KeyNotFoundException"></exception>
        public string GenerateToken(User user)
        {
            Claim[] claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            ];

            SymmetricSecurityKey key = new(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Token") ?? throw new KeyNotFoundException("Token not found.")));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: _currentUtcTimeProvider.GetCurrentUtcTime().DateTime.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
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
