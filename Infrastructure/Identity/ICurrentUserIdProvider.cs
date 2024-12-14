namespace Infrastructure.Identity
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
        string? GetCurrentUserId();
    }
}
