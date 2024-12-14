using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    /// <summary>
    /// Provides the current user.
    /// </summary>
    /// <param name="currentUserIdProvider">Provider of the current user ID.</param>
    /// <param name="userManager">Manager to handle user-related operations.</param>
    public class CurrentUserProvider(
        ICurrentUserIdProvider currentUserIdProvider,
        UserManager<ApplicationUser> userManager)
        : ICurrentUserProvider
    {
        private readonly ICurrentUserIdProvider _currentUserIdProvider = currentUserIdProvider;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        /// <inheritdoc />
        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            string? userId = _currentUserIdProvider.GetCurrentUserId();
            if (userId == null)
                return null;

            return await _userManager.FindByIdAsync(userId);
        }
    }
}
