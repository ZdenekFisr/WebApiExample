using Domain.Entities;

namespace Infrastructure.Services.CurrentUser
{
    /// <summary>
    /// Provides the current user.
    /// </summary>
    /// <param name="currentUserIdProvider">Provider of the current user ID.</param>
    /// <param name="userManager">Manager to handle user-related operations.</param>
    public class CurrentUserProvider(
        ApplicationDbContext dbContext,
        ICurrentUserIdProvider currentUserIdProvider)
        : ICurrentUserProvider
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly ICurrentUserIdProvider _currentUserIdProvider = currentUserIdProvider;

        /// <inheritdoc />
        public async Task<User?> GetCurrentUserAsync()
        {
            string? userId = await _currentUserIdProvider.GetCurrentUserIdAsync();
            if (userId == null)
                return null;

            return await _dbContext.Users.FindAsync(userId);
        }
    }
}
