using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.GenericControllers;
using WebApiExample.GenericRepositories.SimpleModelWithUser;
using WebApiExample.SharedServices.User;

namespace WebApiExample.Features.RailVehicles.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RailVehiclesController(
        ISimpleModelWithUserRepository<RailVehicleModel> modelRepository,
        IUserRepository userRepository)
        : SimpleModelWithUserController<RailVehicleModel>(modelRepository, userRepository)
    {
    }
}
