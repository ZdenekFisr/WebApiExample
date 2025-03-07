using Application.Features.RailVehicles.Repository;
using Asp.Versioning;
using Infrastructure.Exceptions;
using Infrastructure.Services.CurrentUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.RailVehicles.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/rail-vehicles")]
    [ApiController]
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
            string currentUserId;
            try
            {
                currentUserId = _currentUserIdProvider.GetCurrentUserId(Constants.AllPayingRoles);
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(await _repository.GetDrivingVehiclesAsync(currentUserId));
        }

        [HttpGet("pulled")]
        [EndpointDescription("Gets all pulled rail vehicles that are not deleted and belong to the current user.")]
        public async Task<IActionResult> GetAllPulledAsync()
        {
            string currentUserId;
            try
            {
                currentUserId = _currentUserIdProvider.GetCurrentUserId(Constants.AllPayingRoles);
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(await _repository.GetPulledVehiclesAsync(currentUserId));
        }

        [HttpDelete("{id}")]
        [EndpointDescription("Soft deletes a rail vehicle by ID.")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            string currentUserId;
            try
            {
                currentUserId = _currentUserIdProvider.GetCurrentUserId(Constants.AllPayingRoles);
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            await _repository.SoftDeleteAsync(id, currentUserId);
            return Ok();
        }
    }
}
