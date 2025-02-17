using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Asp.Versioning;
using Infrastructure.Identity;
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
            string? currentUserId = _currentUserIdProvider.GetCurrentUserId();
            if (currentUserId is null)
                return Unauthorized();

            return Ok(await _repository.GetManyAsync(currentUserId));
        }

        [HttpPut("{id}")]
        [EndpointDescription("Restores a soft deleted train by ID.")]
        public async Task<IActionResult> RestoreAsync(Guid id)
        {
            string? currentUserId = _currentUserIdProvider.GetCurrentUserId();
            if (currentUserId is null)
                return Unauthorized();

            await _repository.RestoreAsync(id, currentUserId);
            return Ok();
        }

        [HttpDelete("{id}")]
        [EndpointDescription("Hard deletes a train by ID.")]
        public async Task<IActionResult> HardDeleteAsync(Guid id)
        {
            string? currentUserId = _currentUserIdProvider.GetCurrentUserId();
            if (currentUserId is null)
                return Unauthorized();

            await _repository.HardDeleteAsync(id, currentUserId);
            return Ok();
        }
    }
}
