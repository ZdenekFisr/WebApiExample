using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.GeneralServices.RestoreItem;

namespace WebApiExample.Features.RailVehicles
{
    [Authorize]
    [Route("api/[controller]")]
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
