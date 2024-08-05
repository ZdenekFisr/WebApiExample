using Microsoft.AspNetCore.Mvc;
using WebApiExample.GenericRepositories.SimpleModel;

namespace WebApiExample.GenericControllers
{
    /// <summary>
    /// Generic controller that uses registered implementation of <see cref="ISimpleModelRepository{TModel}"/> for CRUD operations. It can be inherited and expanded by some other controller.
    /// </summary>
    /// <typeparam name="TModel">Type of model that is being handled.</typeparam>
    /// <param name="modelRepository">Repository instance that is used for handling the model.</param>
    [ApiController]
    public class SimpleModelController<TModel>(ISimpleModelRepository<TModel> modelRepository) : ControllerBase
        where TModel : Model
    {
        protected readonly ISimpleModelRepository<TModel> _modelRepository = modelRepository;

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetOneByIdAsync(Guid id)
        {
            TModel? result = await _modelRepository.GetOneAsync(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public virtual async Task CreateAsync(TModel model)
            => await _modelRepository.CreateAsync(model);

        [HttpPut]
        public virtual async Task UpdateAsync(Guid id, TModel model)
            => await _modelRepository.UpdateAsync(id, model);

        [HttpDelete]
        public virtual async Task DeleteAsync(Guid id)
            => await _modelRepository.DeleteAsync(id);
    }
}
