using Application.Features.RailVehicles.ListModel;
using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Application.GenericRepositories;
using Asp.Versioning;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.GenericControllers;
using WebApiExample.Services.VerifyUser;

namespace WebApiExample.Features.RailVehicles.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RailVehiclesController(
        IRailVehicleListRepository<RailVehicleListModel> vehicleListRepository,
        ICurrentUserIdProvider currentUserIdProvider)
        : ControllerBase
    {
        private readonly IRailVehicleListRepository<RailVehicleListModel> _repository = vehicleListRepository;
        private readonly ICurrentUserIdProvider _currentUserIdProvider = currentUserIdProvider;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            string? currentUserId = _currentUserIdProvider.GetCurrentUserId();
            if (currentUserId is null)
                return Unauthorized();

            return Ok(await _repository.GetManyAsync(currentUserId));
        }
    }
}
