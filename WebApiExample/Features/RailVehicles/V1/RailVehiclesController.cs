using Application.Features.RailVehicles.Repository;
using Asp.Versioning;
using Infrastructure.Services.CurrentUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.RailVehicles.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/rail-vehicles")]
    [ApiController]
    [Authorize]
    public class RailVehiclesController(
        IRailVehicleListRepository repository,
        ICurrentUserIdProvider currentUserIdProvider)
        : ControllerBase
    {
        private readonly IRailVehicleListRepository _repository = repository;
        private readonly ICurrentUserIdProvider _currentUserIdProvider = currentUserIdProvider;

        [HttpGet("driving")]
        [EndpointDescription("Gets all driving rail vehicles that are not deleted and belong to the current user.")]
        public async Task<IActionResult> GetAllDrivingAsync()
        {
            string? currentUserId = await _currentUserIdProvider.GetCurrentUserIdAsync();
            if (currentUserId is null)
                return Unauthorized();

            return Ok(await _repository.GetDrivingVehiclesAsync(currentUserId));
        }

        [HttpGet("pulled")]
        [EndpointDescription("Gets all pulled rail vehicles that are not deleted and belong to the current user.")]
        public async Task<IActionResult> GetAllPulledAsync()
        {
            string? currentUserId = await _currentUserIdProvider.GetCurrentUserIdAsync();
            if (currentUserId is null)
                return Unauthorized();

            return Ok(await _repository.GetPulledVehiclesAsync(currentUserId));
        }

        [HttpDelete("{id}")]
        [EndpointDescription("Soft deletes a rail vehicle by ID.")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            string? currentUserId = await _currentUserIdProvider.GetCurrentUserIdAsync();
            if (currentUserId is null)
                return Unauthorized();

            await _repository.SoftDeleteAsync(id, currentUserId);
            return Ok();
        }
    }
}
