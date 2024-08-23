using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebApiExample.Authentication
{
    public class ClaimsTransformationService(
        UserManager<ApplicationUser> userManager)
        : IClaimsTransformation
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated != true)
                return principal;

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return principal;

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return principal;

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Any())
                return principal;

            foreach (var role in roles)
            {
                if (principal.HasClaim(ClaimTypes.Role, role))
                    continue;

                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));
            }
            return principal;
        }
    }
}
