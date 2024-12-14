namespace Infrastructure.Identity
{
    /// <summary>
    /// Provides the current user.
    /// </summary>
    public interface ICurrentUserProvider
    {
        /// <summary>
        /// Asynchronously gets the current user.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the current <see cref="ApplicationUser"/> if found; otherwise, null.</returns>
        Task<ApplicationUser?> GetCurrentUserAsync();
    }
}
