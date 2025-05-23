﻿using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Asp.Versioning;
using Infrastructure.Exceptions;
using Infrastructure.Services.CurrentUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.RailVehicles.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/train")]
    [ApiController]
    public class TrainController(
        ITrainRepository<TrainInputModel, TrainOutputModel> repository,
        ICurrentUserIdProvider currentUserIdProvider)
        : ControllerBase
    {
        private readonly ITrainRepository<TrainInputModel, TrainOutputModel> _repository = repository;
        private readonly ICurrentUserIdProvider _currentUserIdProvider = currentUserIdProvider;

        [HttpGet("{id}")]
        [EndpointDescription("Gets a train by ID.")]
        public async Task<IActionResult> GetOneAsync(Guid id)
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

            var train = await _repository.GetOneAsync(id, currentUserId);
            if (train is null)
                return NotFound();

            return Ok(train);
        }

        [HttpPost]
        [EndpointDescription("Creates a new train.")]
        public async Task<IActionResult> CreateAsync(TrainInputModel model)
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
                await _repository.CreateAsync(model, currentUserId);
            }
            catch (VehicleForeignKeyException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        [EndpointDescription("Updates an existing train by ID.")]
        public async Task<IActionResult> UpdateAsync(Guid id, TrainInputModel model)
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
                await _repository.UpdateAsync(id, model, currentUserId);
            }
            catch (VehicleForeignKeyException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
