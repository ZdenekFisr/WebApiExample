using Application.Features.Auth.Model;
using Application.Features.Auth.Repository;
using Application.Services;
using Domain.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Services.Jwt;
using Infrastructure.Services.PasswordHash;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.Auth.Repository
{
    /// <summary>
    /// Repository for authentication operations.
    /// </summary>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="passwordHashService">The password hashing service.</param>
    /// <param name="currentUtcTimeProvider">The provider to get the current UTC time.</param>
    /// <param name="jwtService">The JSON Web Token generating service.</param>
    public class AuthRepository(
        ApplicationDbContext dbContext,
        IPasswordHashService passwordHashService,
        ICurrentUtcTimeProvider currentUtcTimeProvider,
        IJwtService jwtService)
        : IAuthRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IPasswordHashService _passwordHashService = passwordHashService;
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider = currentUtcTimeProvider;
        private readonly IJwtService _jwtService = jwtService;

        /// <inheritdoc />
        public async Task RegisterAsync(UserRegisterModel userModel)
        {
            if (await _dbContext.Users.AnyAsync(u => u.UserName == userModel.UserName))
            {
                throw new UserAlreadyExistsException();
            }

            User user = new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userModel.UserName,
                Email = userModel.Email
            };
            user.PasswordHash = _passwordHashService.HashPassword(user, userModel.Password);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="InvalidPasswordException"></exception>
        public async Task<TokenResponseModel> LoginAsync(UserLoginModel userModel)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userModel.UserName)
                ?? throw new UserNotFoundException();

            if (!_passwordHashService.VerifyPassword(user, userModel.Password))
            {
                throw new InvalidPasswordException();
            }
            return await CreateTokenResponseAsync(user);
        }

        /// <summary>
        /// Creates a token response for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Token response.</returns>
        private async Task<TokenResponseModel> CreateTokenResponseAsync(User user)
        {
            return new()
            {
                AccessToken = _jwtService.GenerateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }

        /// <summary>
        /// Generates a refresh token and saves it to the user entity.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The generated refresh token.</returns>
        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            string refreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = _currentUtcTimeProvider.GetCurrentUtcTime().DateTime.AddDays(7);

            await _dbContext.SaveChangesAsync();
            return refreshToken;
        }

        /// <inheritdoc />
        public async Task<TokenResponseModel> RefreshTokensAsync(RefreshTokenRequestModel request)
        {
            User user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            return await CreateTokenResponseAsync(user);
        }

        /// <summary>
        /// Validates a refresh token.
        /// </summary>
        /// <param name="userId">User's ID.</param>
        /// <param name="refreshToken">Refresh token to validate.</param>
        /// <returns>The user whose refresh token is being validated.</returns>
        /// <exception cref="InvalidRefreshTokenException"></exception>
        private async Task<User> ValidateRefreshTokenAsync(string userId, string refreshToken)
        {
            User? user = await _dbContext.Users.FindAsync(userId);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime < _currentUtcTimeProvider.GetCurrentUtcTime().DateTime)
            {
                throw new InvalidRefreshTokenException();
            }
            return user;
        }
    }
}
