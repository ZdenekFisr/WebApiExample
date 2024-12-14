using Application.Features.RailVehicles.Model;
using Application.GenericRepositories;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.GenericControllers;
using WebApiExample.Services.VerifyUser;

namespace WebApiExample.Features.RailVehicles.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RailVehiclesController(
        ISimpleModelWithUserRepository<RailVehicleModelBase> modelRepository,
        IVerifyUserService userRepository)
        : SimpleModelWithUserController<RailVehicleModelBase>(modelRepository, userRepository)
    {
    }
}
