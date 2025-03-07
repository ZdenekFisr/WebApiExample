using Domain.Entities;

namespace Infrastructure.Services.CurrentUser
{
    /// <summary>
    /// Provides the current user.
    /// </summary>
    public interface ICurrentUserProvider
    {
        /// <summary>
        /// Asynchronously gets the current user.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the current <see cref="User"/>.</returns>
        Task<User?> GetCurrentUserAsync();
    }
}
