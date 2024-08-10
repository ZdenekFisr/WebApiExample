using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApiExample.SharedServices.User
{
    /// <inheritdoc cref="IUserRepository"/>
    public class UserRepository(
        ApplicationDbContext context)
        : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        /// <inheritdoc />
        public async Task<object?> GetUserOrReturnErrorAsync(ControllerBase controller)
        {
            string? userName = controller.HttpContext.User.Identity?.Name;
            if (userName is null)
                return controller.Unauthorized();

            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        /// <inheritdoc />
        public async Task<object> GetUserIdOrReturnErrorAsync(ControllerBase controller)
        {
            var result = await GetUserOrReturnErrorAsync(controller);
            if (result is null)
                return controller.NotFound();

            if (result is IActionResult actionResult)
                return actionResult;

            return ((ApplicationUser)result).Id;
        }
    }
}
