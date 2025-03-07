using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Asp.Versioning;
using Infrastructure.Exceptions;
using Infrastructure.Services.CurrentUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.RailVehicles.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/trains")]
    [ApiController]
    public class TrainsController(
        ITrainListRepository<TrainListModel> repository,
        ICurrentUserIdProvider currentUserIdProvider)
        : ControllerBase
    {
        private readonly ITrainListRepository<TrainListModel> _repository = repository;
        private readonly ICurrentUserIdProvider _currentUserIdProvider = currentUserIdProvider;

        [HttpGet]
        [EndpointDescription("Gets all trains that are not deleted that belong to the current user.")]
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

        [HttpDelete("{id}")]
        [EndpointDescription("Soft deletes a train by ID.")]
        public async Task<IActionResult> SoftDeleteAsync(Guid id)
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
