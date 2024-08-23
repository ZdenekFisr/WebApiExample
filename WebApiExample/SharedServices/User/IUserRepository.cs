using Microsoft.AspNetCore.Mvc;
using WebApiExample.Authentication;

namespace WebApiExample.SharedServices.User
{
    /// <summary>
    /// Contains methods for finding a user in DB by current user's name.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Finds the current user in DB using <see cref="ControllerBase.HttpContext"/>
        /// </summary>
        /// <param name="controller">Controller where the HTTP request is defined.</param>
        /// <returns>
        /// <para>An object representing the user.</para>
        /// <para>If the HTTP request is unauthorized, returns <see cref="ControllerBase.Unauthorized()"/>.</para>
        /// <para>If the user is not found in the DB, returns <see cref="ControllerBase.NotFound()"/>.</para>
        /// </returns>
        Task<object> GetUserOrReturnErrorAsync(ControllerBase controller);

        /// <summary>
        /// Finds the current user's ID in DB using <see cref="ControllerBase.HttpContext"/>.
        /// </summary>
        /// <param name="controller">Controller where the HTTP request is defined.</param>
        /// <returns>
        /// <para>User's ID.</para>
        /// <para>If the HTTP request is unauthorized, returns <see cref="ControllerBase.Unauthorized()"/>.</para>
        /// <para>If the user is not found in the DB, returns <see cref="ControllerBase.NotFound()"/>.</para>
        /// </returns>
        Task<object> GetUserIdOrReturnErrorAsync(ControllerBase controller);

        /// <summary>
        /// Finds a user in DB based on user name.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns>An object representing the user if the user is found in the DB; otherwise, null.</returns>
        Task<ApplicationUser?> GetUserAsync(string userName);
    }
}
