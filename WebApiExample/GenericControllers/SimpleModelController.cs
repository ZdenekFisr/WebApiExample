using Microsoft.AspNetCore.Mvc;
using WebApiExample.GenericRepositories.SimpleModel;

namespace WebApiExample.GenericControllers
{
    /// <summary>
    /// Generic controller that uses registered implementation of <see cref="ISimpleModelRepository{TModel}"/> for CRUD operations. It can be inherited and expanded by some other controller.
    /// </summary>
    /// <typeparam name="TModel">Type of model that is being handled.</typeparam>
    /// <param name="repository">Repository instance that is used for handling the model.</param>
    [ApiController]
    public class SimpleModelController<TModel>(ISimpleModelRepository<TModel> repository) : ControllerBase
        where TModel : Model
    {
        private readonly ISimpleModelRepository<TModel> _repository = repository;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneByIdAsync(Guid id)
        {
            TModel? result = await _repository.GetOneAsync(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task CreateAsync(TModel model)
            => await _repository.CreateAsync(model);

        [HttpPut]
        public async Task UpdateAsync(Guid id, TModel model)
            => await _repository.UpdateAsync(id, model);

        [HttpDelete]
        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
