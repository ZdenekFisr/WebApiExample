using Domain.Entities;
using Infrastructure.Exceptions;

namespace Infrastructure.Services.CurrentUser
{
    /// <summary>
    /// Provides the current user.
    /// </summary>
    /// <param name="dbContext">The application database context.</param>
    /// <param name="currentUserIdProvider">Provider of the current user ID.</param>
    public class CurrentUserProvider(
        ApplicationDbContext dbContext,
        ICurrentUserIdProvider currentUserIdProvider)
        : ICurrentUserProvider
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly ICurrentUserIdProvider _currentUserIdProvider = currentUserIdProvider;

        /// <inheritdoc />
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="UnauthorizedException"></exception>
        /// <exception cref="ForbiddenException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<User?> GetCurrentUserAsync()
        {
            string userId;
            try
            {
                userId = _currentUserIdProvider.GetCurrentUserId();
            }
            catch (Exception)
            {
                throw;
            }

            return await _dbContext.Users.FindAsync(userId);
        }
    }
}
