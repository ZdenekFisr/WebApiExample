using Application.Services;
using Asp.Versioning;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.Services.VerifyUser;

namespace WebApiExample.Features.RailVehicles.V1
{
    [Authorize(Roles = "Admin")]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RailVehiclesAdminController(
        IVerifyUserService userRepository,
        IRestoreItemService<RailVehicle> restoreItemService)
        : ControllerBase
    {
        private readonly IVerifyUserService _userRepository = userRepository;
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
