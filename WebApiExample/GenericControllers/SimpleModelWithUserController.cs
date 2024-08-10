using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.GenericRepositories.SimpleModelWithUser;
using WebApiExample.SharedServices.User;

namespace WebApiExample.GenericControllers
{
    /// <summary>
    /// Generic controller that uses registered implementation of <see cref="ISimpleModelWithUserRepository{TModel}"/> for authorized CRUD operations. It can be inherited and expanded by some other controller.
    /// </summary>
    /// <typeparam name="TModel">Type of model that is being handled.</typeparam>
    /// <param name="modelRepository">Repository instance that is used for handling the model.</param>
    /// <param name="userRepository">Instance of registered implementation of <see cref="IUserRepository"/>.</param>
    [Authorize]
    [ApiController]
    public class SimpleModelWithUserController<TModel>(
        ISimpleModelWithUserRepository<TModel> modelRepository,
        IUserRepository userRepository)
        : ControllerBase
        where TModel : Model
    {
        protected readonly ISimpleModelWithUserRepository<TModel> _modelRepository = modelRepository;
        protected readonly IUserRepository _userRepository = userRepository;

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetOneByIdAsync(Guid id)
        {
            var currentUserId = await _userRepository.GetUserIdOrReturnErrorAsync(this);
            if (currentUserId is IActionResult actionResult)
                return actionResult;

            TModel? result = await _modelRepository.GetOneAsync(id, (string)currentUserId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync(TModel model)
        {
            var currentUserId = await _userRepository.GetUserIdOrReturnErrorAsync(this);
            if (currentUserId is IActionResult actionResult)
                return actionResult;

            await _modelRepository.CreateAsync(model, (string)currentUserId);
            return Ok();
        }

        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync(Guid id, TModel model)
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
}
