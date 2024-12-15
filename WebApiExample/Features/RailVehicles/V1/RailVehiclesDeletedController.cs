using Application.Features.RailVehicles.ListModel;
using Application.Features.RailVehicles.Repository;
using Asp.Versioning;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.RailVehicles.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RailVehiclesDeletedController(
        IRailVehicleDeletedRepository<RailVehicleListModel> repository,
        ICurrentUserIdProvider currentUserIdProvider)
        : ControllerBase
    {
        private readonly IRailVehicleDeletedRepository<RailVehicleListModel> _repository = repository;
        private readonly ICurrentUserIdProvider _currentUserIdProvider = currentUserIdProvider;

        [HttpGet]
        [EndpointDescription("Gets all rail vehicles that are soft deleted and belong to the current user.")]
        public async Task<IActionResult> GetAllAsync()
        {
            string? currentUserId = _currentUserIdProvider.GetCurrentUserId();
            if (currentUserId is null)
                return Unauthorized();

            return Ok(await _repository.GetManyAsync(currentUserId));
        }

        [HttpPut("{id}")]
        [EndpointDescription("Restores a soft deleted rail vehicle by ID.")]
        public async Task<IActionResult> RestoreAsync(Guid id)
        {
            string? currentUserId = _currentUserIdProvider.GetCurrentUserId();
            if (currentUserId is null)
                return Unauthorized();

            await _repository.RestoreAsync(id, currentUserId);
            return Ok();
        }
    }
}
