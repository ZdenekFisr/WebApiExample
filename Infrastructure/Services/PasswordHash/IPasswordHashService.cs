using Domain.Entities;

namespace Infrastructure.Services.PasswordHash
{
    /// <summary>
    /// Represents a service for hashing and verifying passwords.
    /// </summary>
    public interface IPasswordHashService
    {
        /// <summary>
        /// Hashes the specified password for the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>Password hash.</returns>
        string HashPassword(User user, string password);

        /// <summary>
        /// Verifies the specified password for the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if the password is successfully verified; otherwise, false.</returns>
        bool VerifyPassword(User user, string password);
    }
}