using Microsoft.AspNetCore.Mvc;
using WebApiExample.GenericControllers;
using WebApiExample.GenericRepositories.SimpleModelWithUser;
using WebApiExample.SharedServices.User;

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
