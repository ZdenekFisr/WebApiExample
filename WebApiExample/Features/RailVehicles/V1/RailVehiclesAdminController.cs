using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.SharedServices.RestoreItem;
using WebApiExample.SharedServices.User;

namespace WebApiExample.Features.RailVehicles.V1
{
    [Authorize(Roles = "Admin")]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RailVehiclesAdminController(
        IUserRepository userRepository,
        IRestoreItemService<RailVehicle> restoreItemService)
        : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRestoreItemService<RailVehicle> _restoreItemService = restoreItemService;

        [HttpPut("restore")]
        [EndpointDescription("Restores a soft-deleted rail vehicle.")]
        public async Task<IActionResult> Restore(Guid id)
        {
            var currentUserId = await _userRepository.GetUserIdOrReturnErrorAsync(this);
            if (currentUserId is IActionResult actionResult)
                return actionResult;

            await _restoreItemService.RestoreAsync(id, (string)currentUserId);
            return Ok();
        }
    }
}
