namespace WebApiExample.SharedServices.User
{
    /// <summary>
    /// Contains methods for finding a user in DB by user name.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Finds a user in DB by user name.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns>User info if the user is found; otherwise, null.</returns>
        public Task<ApplicationUser?> GetUserByNameAsync(string? userName);

        /// <summary>
        /// Finds a user ID in DB by user name.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns>User ID if the user is found; otherwise, null.</returns>
        public Task<string?> GetUserIdByNameAsync(string? userName);
    }
}
