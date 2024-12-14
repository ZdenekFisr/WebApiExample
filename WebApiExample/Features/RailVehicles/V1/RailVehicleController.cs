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

        [HttpGet("{id}")]
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
        public async Task<IActionResult> CreatePulledAsync(RailVehiclePulledModel model)
            => await CreateAsync(model);

        [HttpPost("dependent")]
        public async Task<IActionResult> CreateDependentAsync(RailVehicleDependentModel model)
            => await CreateAsync(model);

        [HttpPost("independent")]
        public async Task<IActionResult> CreateIndependentAsync(RailVehicleIndependentModel model)
            => await CreateAsync(model);

        [HttpPost("hybrid")]
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
        public async Task<IActionResult> UpdatePulledAsync(Guid id, RailVehiclePulledModel model)
            => await UpdateAsync(id, model);

        [HttpPut("dependent/{id}")]
        public async Task<IActionResult> UpdateDependentAsync(Guid id, RailVehicleDependentModel model)
            => await UpdateAsync(id, model);

        [HttpPut("independent/{id}")]
        public async Task<IActionResult> UpdateIndependentAsync(Guid id, RailVehicleIndependentModel model)
            => await UpdateAsync(id, model);

        [HttpPut("hybrid/{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            string? currentUserId = _currentUserIdProvider.GetCurrentUserId();
            if (currentUserId is null)
                return Unauthorized();

            await _repository.SoftDeleteAsync(id, currentUserId);
            return Ok();
        }
    }
}
