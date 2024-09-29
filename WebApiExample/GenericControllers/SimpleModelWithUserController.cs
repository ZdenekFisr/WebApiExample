using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.GenericRepositories.SimpleModelWithUser;
using WebApiExample.SharedServices.User;

namespace WebApiExample.GenericControllers
{
    /// <summary>
    /// Generic controller that uses registered implementation of <see cref="ISimpleModelWithUserRepository{TInputModel, TOutputModel}"/> for authorized CRUD operations. It can be inherited and expanded by some other controller.
    /// </summary>
    /// <typeparam name="TInputModel">Type of input model that is being handled. It is used to create or update a DB item.</typeparam>
    /// <typeparam name="TOutputModel">Type of output model that is being handled. It is used to get a DB item.</typeparam>
    /// <param name="modelRepository">Repository instance that is used for handling the model.</param>
    /// <param name="userRepository">Instance of registered implementation of <see cref="IUserRepository"/>.</param>
    [Authorize]
    [ApiController]
    public class SimpleModelWithUserController<TInputModel, TOutputModel>(
        ISimpleModelWithUserRepository<TInputModel, TOutputModel> modelRepository,
        IUserRepository userRepository)
        : ControllerBase
        where TInputModel : Model
        where TOutputModel : Model
    {
        protected readonly ISimpleModelWithUserRepository<TInputModel, TOutputModel> _modelRepository = modelRepository;
        protected readonly IUserRepository _userRepository = userRepository;

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetOneByIdAsync(Guid id)
        {
            var currentUserId = await _userRepository.GetUserIdOrReturnErrorAsync(this);
            if (currentUserId is IActionResult actionResult)
                return actionResult;

            TOutputModel? result = await _modelRepository.GetOneAsync(id, (string)currentUserId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync(TInputModel model)
        {
            var currentUserId = await _userRepository.GetUserIdOrReturnErrorAsync(this);
            if (currentUserId is IActionResult actionResult)
                return actionResult;

            await _modelRepository.CreateAsync(model, (string)currentUserId);
            return Ok();
        }

        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync(Guid id, TInputModel model)
        {
            var currentUserId = await _userRepository.GetUserIdOrReturnErrorAsync(this);
            if (currentUserId is IActionResult actionResult)
                return actionResult;

            await _modelRepository.UpdateAsync(id, model, (string)currentUserId);
            return Ok();
        }

        [HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            var currentUserId = await _userRepository.GetUserIdOrReturnErrorAsync(this);
            if (currentUserId is IActionResult actionResult)
                return actionResult;

            await _modelRepository.DeleteAsync(id, (string)currentUserId);
            return Ok();
        }
    }

    /// <summary>
    /// Generic controller that uses registered implementation of <see cref="ISimpleModelWithUserRepository{TInputModel, }"/> for authorized CRUD operations. It can be inherited and expanded by some other controller.
    /// </summary>
    /// <typeparam name="TModel">Type of model that is being handled.</typeparam>
    /// <param name="modelRepository">Repository instance that is used for handling the model.</param>
    /// <param name="userRepository">Instance of registered implementation of <see cref="IUserRepository"/>.</param>
    public class SimpleModelWithUserController<TModel>(
        ISimpleModelWithUserRepository<TModel, TModel> modelRepository,
        IUserRepository userRepository)
        : SimpleModelWithUserController<TModel, TModel>(modelRepository, userRepository)
        where TModel : Model
    {
    }
}
