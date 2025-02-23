namespace Infrastructure.Services.CurrentUser
{
    /// <summary>
    /// Provides the ID of the current user.
    /// </summary>
    public interface ICurrentUserIdProvider
    {
        /// <summary>
        /// Gets the ID of the current user.
        /// </summary>
        /// <returns>The ID of the current user if found; otherwise, null.</returns>
        Task<string?> GetCurrentUserIdAsync();
    }
}
