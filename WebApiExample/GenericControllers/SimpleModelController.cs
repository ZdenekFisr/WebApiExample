using Application.Common;
using Application.GenericRepositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.GenericControllers
{
    /// <summary>
    /// Generic controller that uses registered implementation of <see cref="ISimpleModelRepository{TInputModel, TOutputModel}"/> for CRUD operations. It can be inherited and expanded by some other controller.
    /// </summary>
    /// <typeparam name="TInputModel">Type of input model that is being handled. It is used to create or update a DB item.</typeparam>
    /// <typeparam name="TOutputModel">Type of output model that is being handled. It is used to get a DB item.</typeparam>
    /// <param name="modelRepository">Repository instance that is used for handling the model.</param>
    [ApiController]
    public class SimpleModelController<TInputModel, TOutputModel>(
        ISimpleModelRepository<TInputModel, TOutputModel> modelRepository)
        : ControllerBase
        where TInputModel : Model
        where TOutputModel : Model
    {
        protected readonly ISimpleModelRepository<TInputModel, TOutputModel> _modelRepository = modelRepository;

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetOneByIdAsync(Guid id)
        {
            TOutputModel? result = await _modelRepository.GetOneAsync(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public virtual async Task CreateAsync(TInputModel model)
            => await _modelRepository.CreateAsync(model);

        [HttpPut]
        public virtual async Task UpdateAsync(Guid id, TInputModel model)
            => await _modelRepository.UpdateAsync(id, model);

        [HttpDelete]
        public virtual async Task DeleteAsync(Guid id)
            => await _modelRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Generic controller that uses registered implementation of <see cref="ISimpleModelRepository{TInputModel, TOutputModel}"/> for CRUD operations. It can be inherited and expanded by some other controller.
    /// </summary>
    /// <typeparam name="TModel">Type of model that is being handled.</typeparam>
    /// <param name="modelRepository">Repository instance that is used for handling the model.</param>
    public class SimpleModelController<TModel>(
        ISimpleModelRepository<TModel, TModel> modelRepository)
        : SimpleModelController<TModel, TModel>(modelRepository)
        where TModel : Model
    {
    }
}
