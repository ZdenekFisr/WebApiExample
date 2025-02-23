using Domain.Entities;

namespace Infrastructure.Services.Jwt
{
    /// <summary>
    /// Represents a service for generating JWT tokens.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The generated JWT.</returns>
        string GenerateToken(User user);

        /// <summary>
        /// Generates a refresh token.
        /// </summary>
        /// <returns>The generated refresh token.</returns>
        string GenerateRefreshToken();
    }
}