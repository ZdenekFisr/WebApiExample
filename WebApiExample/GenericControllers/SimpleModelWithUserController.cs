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
            string? currentUserName = HttpContext.User.Identity?.Name;
            if (currentUserName is null)
                return Unauthorized();

            string? currentUserId = await _userRepository.GetUserIdByNameAsync(currentUserName);
            if (currentUserId is null)
                return NotFound();

            TModel? result = await _modelRepository.GetOneAsync(id, currentUserId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync(TModel model)
        {
            string? currentUserName = HttpContext.User.Identity?.Name;
            if (currentUserName is null)
                return Unauthorized();

            string? currentUserId = await _userRepository.GetUserIdByNameAsync(currentUserName);
            if (currentUserId is null)
                return NotFound();

            await _modelRepository.CreateAsync(model, currentUserId);
            return Ok();
        }

        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync(Guid id, TModel model)
        {
            string? currentUserName = HttpContext.User.Identity?.Name;
            if (currentUserName is null)
                return Unauthorized();

            string? currentUserId = await _userRepository.GetUserIdByNameAsync(currentUserName);
            if (currentUserId is null)
                return NotFound();

            await _modelRepository.UpdateAsync(id, model, currentUserId);
            return Ok();
        }

        [HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            string? currentUserName = HttpContext.User.Identity?.Name;
            if (currentUserName is null)
                return Unauthorized();

            string? currentUserId = await _userRepository.GetUserIdByNameAsync(currentUserName);
            if (currentUserId is null)
                return NotFound();

            await _modelRepository.DeleteAsync(id, currentUserId);
            return Ok();
        }
    }
}
