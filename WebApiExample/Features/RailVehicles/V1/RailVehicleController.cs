using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Asp.Versioning;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.RailVehicles.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RailVehicleController(
        IRailVehicleRepository<RailVehicleModelBase> repository,
        ICurrentUserIdProvider currentUserIdProvider)
        : ControllerBase
    {
        private readonly IRailVehicleRepository<RailVehicleModelBase> _repository = repository;
        private readonly ICurrentUserIdProvider _currentUserIdProvider = currentUserIdProvider;

        private const string createDescription = "Creates a new";
        private const string updateDescription = "Updates an existing";
        private const string pulledDescription = " pulled (motorless) rail vehicle.";
        private const string dependentDescription = " dependent (requiring electrification) rail vehicle.";
        private const string independentDescription = " independent (not requiring electrification) rail vehicle.";
        private const string hybridDescription = " hybrid (not requiring electrification but able to use it) rail vehicle.";

        [HttpGet("{id}")]
        [EndpointDescription("Gets a rail vehicle by ID.")]
        public async Task<IActionResult> GetOneAsync(Guid id)
        {
            string? currentUserId = _currentUserIdProvider.GetCurrentUserId();
            if (currentUserId is null)
                return Unauthorized();

            var vehicle = await _repository.GetOneAsync(id, currentUserId);
            if (vehicle is null)
                return NotFound();

            return Ok(vehicle);
        }

        [HttpPost("pulled")]
        [EndpointDescription($"{createDescription}{pulledDescription}")]
        public async Task<IActionResult> CreatePulledAsync(RailVehiclePulledModel model)
            => await CreateAsync(model);

        [HttpPost("dependent")]
        [EndpointDescription($"{createDescription}{dependentDescription}")]
        public async Task<IActionResult> CreateDependentAsync(RailVehicleDependentModel model)
            => await CreateAsync(model);

        [HttpPost("independent")]
        [EndpointDescription($"{createDescription}{independentDescription}")]
        public async Task<IActionResult> CreateIndependentAsync(RailVehicleIndependentModel model)
            => await CreateAsync(model);

        [HttpPost("hybrid")]
        [EndpointDescription($"{createDescription}{hybridDescription}")]
        public async Task<IActionResult> CreateHybridAsync(RailVehicleHybridModel model)
            => await CreateAsync(model);

        private async Task<IActionResult> CreateAsync(RailVehicleModelBase model)
        {
            string? currentUserId = _currentUserIdProvider.GetCurrentUserId();
            if (currentUserId is null)
                return Unauthorized();

            await _repository.CreateAsync(model, currentUserId);
            return Ok();
        }

        [HttpPut("pulled/{id}")]
        [EndpointDescription($"{updateDescription}{pulledDescription}")]
        public async Task<IActionResult> UpdatePulledAsync(Guid id, RailVehiclePulledModel model)
            => await UpdateAsync(id, model);

        [HttpPut("dependent/{id}")]
        [EndpointDescription($"{updateDescription}{dependentDescription}")]
        public async Task<IActionResult> UpdateDependentAsync(Guid id, RailVehicleDependentModel model)
            => await UpdateAsync(id, model);

        [HttpPut("independent/{id}")]
        [EndpointDescription($"{updateDescription}{independentDescription}")]
        public async Task<IActionResult> UpdateIndependentAsync(Guid id, RailVehicleIndependentModel model)
            => await UpdateAsync(id, model);

        [HttpPut("hybrid/{id}")]
        [EndpointDescription($"{updateDescription}{hybridDescription}")]
        public async Task<IActionResult> UpdateHybridAsync(Guid id, RailVehicleHybridModel model)
            => await UpdateAsync(id, model);

        private async Task<IActionResult> UpdateAsync(Guid id, RailVehicleModelBase model)
        {
            string? currentUserId = _currentUserIdProvider.GetCurrentUserId();
            if (currentUserId is null)
                return Unauthorized();

            await _repository.UpdateAsync(id, model, currentUserId);
            return Ok();
        }
    }
}
