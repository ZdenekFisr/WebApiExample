using Microsoft.AspNetCore.Mvc;
using WebApiExample.GenericControllers;
using WebApiExample.GenericRepositories.SimpleModel;

namespace WebApiExample.Features.FilmDatabase
{
    [Route("api/[controller]")]
    public class FilmDatabaseController(ISimpleModelRepository<FilmModel> repository) : SimpleModelController<FilmModel>(repository)
    {
    }
}
