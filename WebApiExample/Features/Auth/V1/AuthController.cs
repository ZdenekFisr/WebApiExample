using Application.Features.Auth.Model;
using Application.Features.Auth.Repository;
using Asp.Versioning;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.Auth.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiController]
    public class AuthController(IAuthRepository repository) : ControllerBase
    {
        private readonly IAuthRepository _repository = repository;

        [HttpPost("register")]
        [EndpointDescription("Registers a new user.")]
        public async Task<IActionResult> RegisterAsync(UserRegisterModel userModel)
        {
            await _repository.RegisterAsync(userModel);
            return Ok();
        }

        [HttpPost("login")]
        [EndpointDescription("Logs in a user.")]
        public async Task<IActionResult> LoginAsync(UserLoginModel userModel)
        {
            TokenResponseModel tokenResponse;
            try
            {
                tokenResponse = await _repository.LoginAsync(userModel);
            }
            catch (UserNotFoundException)
            {
                return BadRequest();
            }
            catch (InvalidPasswordException)
            {
                return BadRequest();
            }
            return Ok(tokenResponse);
        }

        [HttpPost("refresh-tokens")]
        [EndpointDescription("Refreshes access and refresh tokens.")]
        public async Task<IActionResult> RefreshTokensAsync(RefreshTokenRequestModel request)
        {
            TokenResponseModel tokenResponse;
            try
            {
                tokenResponse = await _repository.RefreshTokensAsync(request);
            }
            catch (InvalidRefreshTokenException)
            {
                return BadRequest();
            }
            return Ok(tokenResponse);
        }
    }
}
