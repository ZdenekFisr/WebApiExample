using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Asp.Versioning;
using Infrastructure.Exceptions;
using Infrastructure.Services.CurrentUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.RailVehicles.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/rail-vehicle")]
    [ApiController]
    public class RailVehicleController(
        IRailVehicleRepository<RailVehicleModelBase> repository,
        ICurrentUserIdProvider currentUserIdProvider)
        : ControllerBase
    {
        private readonly IRailVehicleRepository<RailVehicleModelBase> _repository = repository;
        private readonly ICurrentUserIdProvider _currentUserIdProvider = currentUserIdProvider;

        [HttpGet("{id}")]
        [EndpointDescription("Gets a rail vehicle by ID.")]
        public async Task<IActionResult> GetOneAsync(Guid id)
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

            var vehicle = await _repository.GetOneAsync(id, currentUserId);
            if (vehicle is null)
                return NotFound();

            return Ok(vehicle);
        }

        [HttpPost("driving")]
        [EndpointDescription("Creates a new driving rail vehicle.")]
        public async Task<IActionResult> CreateDrivingAsync(RailVehicleDrivingModel model)
            => await CreateAsync(model);

        [HttpPost("pulled")]
        [EndpointDescription("Creates a new pulled (motorless) rail vehicle.")]
        public async Task<IActionResult> CreatePulledAsync(RailVehiclePulledModel model)
            => await CreateAsync(model);

        private async Task<IActionResult> CreateAsync(RailVehicleModelBase model)
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

            await _repository.CreateAsync(model, currentUserId);
            return Ok();
        }

        [HttpPut("driving/{id}")]
        [EndpointDescription("Updates an existing driving rail vehicle by ID.")]
        public async Task<IActionResult> UpdateDrivingAsync(Guid id, RailVehicleDrivingModel model)
            => await UpdateAsync(id, model);

        [HttpPut("pulled/{id}")]
        [EndpointDescription("Updates an existing pulled (motorless) rail vehicle by ID.")]
        public async Task<IActionResult> UpdatePulledAsync(Guid id, RailVehiclePulledModel model)
            => await UpdateAsync(id, model);

        private async Task<IActionResult> UpdateAsync(Guid id, RailVehicleModelBase model)
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

            await _repository.UpdateAsync(id, model, currentUserId);
            return Ok();
        }
    }
}
