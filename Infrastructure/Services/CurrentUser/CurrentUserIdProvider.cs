using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.CurrentUser
{
    /// <summary>
    /// Provides the ID of the current user.
    /// </summary>
    /// <param name="configuration">Configuration settings.</param>
    /// <param name="httpContextAccessor">Accessor to get the current HTTP context.</param>
    public class CurrentUserIdProvider(
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor)
        : ICurrentUserIdProvider
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        /// <inheritdoc />
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="UnauthorizedException"></exception>
        /// <exception cref="ForbiddenException"></exception>
        /// <exception cref="Exception"></exception>
        public string GetCurrentUserId(string[]? allowedRoles = null)
        {
            if (_httpContextAccessor.HttpContext == null)
                throw new Exception("HttpContext is null.");

            var authHeader = _httpContextAccessor.HttpContext.Request.Headers.Authorization.FirstOrDefault()
                ?? throw new UnauthorizedException("The header is missing authorization.");

            var token = authHeader.Trim();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Token"]
                ?? throw new KeyNotFoundException("Token not found."));

            ClaimsPrincipal? principal;
            try
            {
                principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
            }
            catch
            {
                throw new UnauthorizedException();
            }

            if (allowedRoles is not null)
            {
                string[] currentUserRoles = principal.FindFirst(ClaimTypes.Role)?.Value.Split(',') ?? [];
                if (!currentUserRoles.Any(role => allowedRoles.Contains(role)))
                    throw new ForbiddenException();
            }

            return principal.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedException("Token is missing user's ID.");
        }
    }
}
