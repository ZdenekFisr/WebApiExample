using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.PasswordHash
{
    /// <summary>
    /// Represents a service for hashing and verifying passwords.
    /// </summary>
    public class PasswordHashService : IPasswordHashService
    {
        /// <inheritdoc />
        public string HashPassword(User user, string password)
            => new PasswordHasher<User>().HashPassword(user, password);

        /// <inheritdoc />
        public bool VerifyPassword(User user, string password)
        {
            return new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Failed;
        }
    }
}
