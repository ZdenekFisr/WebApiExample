using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiExample.Authentication;

namespace WebApiExample.SharedServices.User
{
    /// <inheritdoc cref="IUserRepository"/>
    public class UserRepository(
        ApplicationDbContext context)
        : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        /// <inheritdoc />
        public async Task<object> GetUserOrReturnErrorAsync(ControllerBase controller)
        {
            string? userName = controller.HttpContext.User.Identity?.Name;
            if (userName is null)
                return controller.Unauthorized();

            ApplicationUser? user = await GetUserAsync(userName);
            if (user is null)
                return controller.NotFound();

            return user;
        }

        /// <inheritdoc />
        public async Task<object> GetUserIdOrReturnErrorAsync(ControllerBase controller)
        {
            object result = await GetUserOrReturnErrorAsync(controller);

            if (result is IActionResult actionResult)
                return actionResult;

            return ((ApplicationUser)result).Id;
        }

        /// <inheritdoc />
        public async Task<ApplicationUser?> GetUserAsync(string userName)
            => await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
    }
}
