using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services.CurrentUser
{
    /// <summary>
    /// Provides the ID of the current user.
    /// </summary>
    /// <param name="httpContextAccessor">Accessor to get the current HTTP context.</param>
    public class CurrentUserIdProvider(
        IHttpContextAccessor httpContextAccessor)
        : ICurrentUserIdProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        /// <inheritdoc />
        public async Task<string?> GetCurrentUserIdAsync()
        {
            if (_httpContextAccessor.HttpContext == null)
                return null;

            ClaimsPrincipal? principal = (await _httpContextAccessor.HttpContext.AuthenticateAsync()).Principal;
            return principal?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
