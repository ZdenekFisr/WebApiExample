using Application.Features.Auth.Model;

namespace Application.Features.Auth.Repository
{
    /// <summary>
    /// Interface with authentication method abstractions.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userModel">An object with params needed for registration.</param>
        /// <returns></returns>
        Task RegisterAsync(UserRegisterModel userModel);

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="userModel">An object with credentials.</param>
        /// <returns>Bearer tokens.</returns>
        Task<TokenResponseModel> LoginAsync(UserLoginModel userModel);

        /// <summary>
        /// Refreshes access and refresh tokens.
        /// </summary>
        /// <param name="refreshTokenRequest">An object with parameters needed for token refresh.</param>
        /// <returns>Bearer tokens.</returns>
        Task<TokenResponseModel> RefreshTokensAsync(RefreshTokenRequestModel refreshTokenRequest);
    }
}