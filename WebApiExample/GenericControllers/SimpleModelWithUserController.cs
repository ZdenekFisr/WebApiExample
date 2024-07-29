using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.GeneralServices.User;
using WebApiExample.GenericRepositories.SimpleModelWithUser;

namespace WebApiExample.GenericControllers
{
    /// <summary>
    /// Generic controller that uses registered implementation of <see cref="ISimpleModelWithUserRepository{TModel}"/> for authorized CRUD operations. It can be inherited and expanded by some other controller.
    /// </summary>
    /// <typeparam name="TModel">Type of model that is being handled.</typeparam>
    /// <param name="repository">Repository instance that is used for handling the model.</param>
    /// <param name="currentUserService">Instance of registered implementation of <see cref="IUserRepository"/>.</param>
    [Authorize]
    [ApiController]
    public class SimpleModelWithUserController<TModel>(
        ISimpleModelWithUserRepository<TModel> repository,
        IUserRepository currentUserService)
        : ControllerBase
        where TModel : Model
    {
        private readonly ISimpleModelWithUserRepository<TModel> _repository = repository;
        private readonly IUserRepository _currentUserService = currentUserService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneByIdAsync(Guid id)
        {
            string? currentUserName = HttpContext.User.Identity?.Name;
            if (currentUserName is null)
                return Unauthorized();

            string? currentUserId = await _currentUserService.GetUserIdByNameAsync(currentUserName);
            if (currentUserId is null)
                return NotFound();

            TModel? result = await _repository.GetOneAsync(id, currentUserId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TModel model)
        {
            string? currentUserName = HttpContext.User.Identity?.Name;
            if (currentUserName is null)
                return Unauthorized();

            string? currentUserId = await _currentUserService.GetUserIdByNameAsync(currentUserName);
            if (currentUserId is null)
                return NotFound();

            await _repository.CreateAsync(model, currentUserId);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Guid id, TModel model)
        {
            string? currentUserName = HttpContext.User.Identity?.Name;
            if (currentUserName is null)
                return Unauthorized();

            string? currentUserId = await _currentUserService.GetUserIdByNameAsync(currentUserName);
            if (currentUserId is null)
                return NotFound();

            await _repository.UpdateAsync(id, model, currentUserId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            string? currentUserName = HttpContext.User.Identity?.Name;
            if (currentUserName is null)
                return Unauthorized();

            string? currentUserId = await _currentUserService.GetUserIdByNameAsync(currentUserName);
            if (currentUserId is null)
                return NotFound();

            await _repository.DeleteAsync(id, currentUserId);
            return Ok();
        }
    }
}
