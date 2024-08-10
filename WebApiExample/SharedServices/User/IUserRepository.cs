using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.SharedServices.User
{
    /// <summary>
    /// Contains methods for finding a user in DB by current user's name.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Finds the current user in DB using <see cref="ControllerBase.HttpContext"/>.
        /// </summary>
        /// <param name="controller">Controller where the HTTP request is defined.</param>
        /// <returns>User info if the user is found; otherwise, null. If the HTTP request is unauthorized, returns <see cref="ControllerBase.Unauthorized()"/>.</returns>
        Task<object?> GetUserOrReturnErrorAsync(ControllerBase controller);

        /// <summary>
        /// Finds the current user's ID in DB using <see cref="ControllerBase.HttpContext"/>.
        /// </summary>
        /// <param name="controller">Controller where the HTTP request is defined.</param>
        /// <returns>User's ID. If the HTTP request is unauthorized, returns <see cref="ControllerBase.Unauthorized()"/>. If the user is not found, returns <see cref="ControllerBase.NotFound()"/>.</returns>
        Task<object> GetUserIdOrReturnErrorAsync(ControllerBase controller);
    }
}
