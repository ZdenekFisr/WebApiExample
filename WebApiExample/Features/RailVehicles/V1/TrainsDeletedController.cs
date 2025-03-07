using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Asp.Versioning;
using Infrastructure.Exceptions;
using Infrastructure.Services.CurrentUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.RailVehicles.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/trains-deleted")]
    [ApiController]
    public class TrainsDeletedController(
        ITrainDeletedRepository<TrainDeletedModel> repository,
        ICurrentUserIdProvider currentUserIdProvider)
        : ControllerBase
    {
        private readonly ITrainDeletedRepository<TrainDeletedModel> _repository = repository;
        private readonly ICurrentUserIdProvider _currentUserIdProvider = currentUserIdProvider;

        [HttpGet]
        [EndpointDescription("Gets all trains that are soft deleted and belong to the current user.")]
        public async Task<IActionResult> GetAllAsync()
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

            return Ok(await _repository.GetManyAsync(currentUserId));
        }

        [HttpPut("{id}")]
        [EndpointDescription("Restores a soft deleted train by ID.")]
        public async Task<IActionResult> RestoreAsync(Guid id)
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

            await _repository.RestoreAsync(id, currentUserId);
            return Ok();
        }

        [HttpDelete("{id}")]
        [EndpointDescription("Hard deletes a train by ID.")]
        public async Task<IActionResult> HardDeleteAsync(Guid id)
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

            await _repository.HardDeleteAsync(id, currentUserId);
            return Ok();
        }
    }
}
