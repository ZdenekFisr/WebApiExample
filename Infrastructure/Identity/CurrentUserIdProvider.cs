using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Identity
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
        public string? GetCurrentUserId()
        {
            ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
