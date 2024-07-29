using Microsoft.AspNetCore.Mvc;
using WebApiExample.GeneralServices.User;
using WebApiExample.GenericControllers;
using WebApiExample.GenericRepositories.SimpleModelWithUser;

namespace WebApiExample.Features.RailVehicles
{
    [Route("api/[controller]")]
    public class RailVehiclesController(
        ISimpleModelWithUserRepository<RailVehicleModel> repository,
        IUserRepository currentUserService)
        : SimpleModelWithUserController<RailVehicleModel>(repository, currentUserService)
    {
    }
}
