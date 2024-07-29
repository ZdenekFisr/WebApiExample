using Microsoft.EntityFrameworkCore;

namespace WebApiExample.GeneralServices.User
{
    /// <inheritdoc cref="IUserRepository"/>
    public class UserRepository(
        ApplicationDbContext context)
        : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        /// <inheritdoc />
        public async Task<ApplicationUser?> GetUserByNameAsync(string? userName)
            => await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

        /// <inheritdoc />
        public async Task<string?> GetUserIdByNameAsync(string? userName)
        {
            var user = await GetUserByNameAsync(userName);
            if (user is null)
                return null;

            return user.Id;
        }
    }
}
