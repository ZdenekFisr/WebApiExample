using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Asp.Versioning;
using Infrastructure.Exceptions;
using Infrastructure.Services.CurrentUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.RailVehicles.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/electrification-types")]
    [ApiController]
    public class ElectrificationTypesController(
        IElectrificationTypeRepository<ElectrificationTypeModel, ElectrificationTypeListModel> repository,
        ICurrentUserIdProvider currentUserIdProvider)
        : ControllerBase
    {
        private readonly IElectrificationTypeRepository<ElectrificationTypeModel, ElectrificationTypeListModel> _repository = repository;
        private readonly ICurrentUserIdProvider _currentUserIdProvider = currentUserIdProvider;

        [HttpGet]
        [EndpointDescription("Gets all electrification types that belong to the current user.")]
        public async Task<IActionResult> GetAllAsync()
        {
            string currentUserId;
            try
            {
                currentUserId = _currentUserIdProvider.GetCurrentUserId(Constants.AllPayingRoles);
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var electrificationTypes = await _repository.GetManyAsync(currentUserId);
            return Ok(electrificationTypes);
        }

        [HttpPost]
        [EndpointDescription("Creates a new electrification type.")]
        public async Task<IActionResult> CreateAsync(ElectrificationTypeModel model)
        {
            string currentUserId;
            try
            {
                currentUserId = _currentUserIdProvider.GetCurrentUserId(Constants.AllPayingRoles);
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            await _repository.CreateAsync(model, currentUserId);
            return Ok();
        }

        [HttpPut("{id}")]
        [EndpointDescription("Updates an existing electrification type by ID.")]
        public async Task<IActionResult> UpdateAsync(Guid id, ElectrificationTypeModel model)
        {
            string currentUserId;
            try
            {
                currentUserId = _currentUserIdProvider.GetCurrentUserId(Constants.AllPayingRoles);
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            await _repository.UpdateAsync(id, model, currentUserId);
            return Ok();
        }

        [HttpDelete("{id}")]
        [EndpointDescription("Hard deletes an electrification type by ID.")]
        public async Task<IActionResult> HardDeleteAsync(Guid id)
        {
            string currentUserId;
            try
            {
                currentUserId = _currentUserIdProvider.GetCurrentUserId(Constants.AllPayingRoles);
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            try
            {
                await _repository.HardDeleteAsync(id, currentUserId);
            }
            catch (ElectrificationTypeForeignKeyException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
