using Microsoft.AspNetCore.Mvc;
using WebApiExample.GenericControllers;
using WebApiExample.GenericRepositories;

namespace WebApiExample.Features.FilmDatabase
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmDatabaseController(ISimpleModelRepository<FilmModel> repository) : SimpleController<FilmModel>(repository)
    {
    }
}
