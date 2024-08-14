using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.SharedServices.RestoreItem;

namespace WebApiExample.Features.RailVehicles.V1
{
    [Authorize(Roles = "Admin")]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RailVehiclesAdminController(IRestoreItemService restoreItemService) : ControllerBase
    {
        private readonly IRestoreItemService _restoreItemService = restoreItemService;

        [HttpPut("restore")]
        [EndpointDescription("Restores a soft-deleted rail vehicle.")]
        public async Task Restore(Guid id)
            => await _restoreItemService.Restore(id);
    }
}
