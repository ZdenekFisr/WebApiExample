﻿using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DatabaseOperations.Insert;
using Infrastructure.DatabaseOperations.Restore;
using Infrastructure.DatabaseOperations.SoftDelete;
using Infrastructure.DatabaseOperations.Update;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository class for managing Vehicle entities.
    /// </summary>
    /// <param name="mapper">The mapper to map between entities and DTOs.</param>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="insertRowOperation">The operation to insert a row into the database.</param>
    /// <param name="updateOperation">The operation to update an entity.</param>
    /// <param name="softDeleteOperation">The operation to perform soft delete on an entity.</param>
    /// <param name="restoreOperation">The operation to restore an entity.</param>
    public class RailVehicleRepository(
        IMapper mapper,
        ApplicationDbContext dbContext,
        IInsertOperation<RailVehicleModelBase> insertRowOperation,
        IUpdateOperation<RailVehicle, RailVehicleModelBase> updateOperation,
        ISoftDeleteOperation softDeleteOperation,
        IRestoreOperation restoreOperation)
        : IRailVehicleRepository<RailVehicleModelBase>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IInsertOperation<RailVehicleModelBase> _insertRowOperation = insertRowOperation;
        private readonly IUpdateOperation<RailVehicle, RailVehicleModelBase> _updateOperation = updateOperation;
        private readonly ISoftDeleteOperation _softDeleteOperation = softDeleteOperation;
        private readonly IRestoreOperation _restoreOperation = restoreOperation;

        /// <inheritdoc />
        public async Task<RailVehicleModelBase?> GetOneAsync(Guid id, string userId)
        {
            RailVehicle? entity = await _dbContext.RailVehicles
                .AsNoTracking()
                .Include(v => v.TractionDiagram)
                .FirstOrDefaultAsync(v => v.Id == id && v.UserId == userId && !v.IsDeleted); ;
            if (entity is null)
                return null;

            if (entity.EfficiencyDependent is null)
            {
                if (entity.EfficiencyIndependent is null)
                    return _mapper.Map<RailVehiclePulledModel>(entity);

                return _mapper.Map<RailVehicleIndependentModel>(entity);
            }

            if (entity.EfficiencyIndependent is null)
                return _mapper.Map<RailVehicleDependentModel>(entity);

            return _mapper.Map<RailVehicleHybridModel>(entity);
        }

        /// <inheritdoc />
        public async Task CreateAsync(RailVehicleModelBase dto, string userId)
            => await _insertRowOperation.InsertAsync(_dbContext, dto, userId);

        /// <inheritdoc />
        public async Task UpdateAsync(Guid id, RailVehicleModelBase newDto, string userId)
            => await _updateOperation.UpdateAsync(_dbContext, FindEntityByIdAsync, id, newDto, userId);

        /// <inheritdoc />
        public async Task SoftDeleteAsync(Guid id, string userId)
            => await _softDeleteOperation.SoftDeleteAsync(_dbContext, id, userId);

        /// <inheritdoc />
        public async Task RestoreAsync(Guid id, string userId)
            => await _restoreOperation.RestoreAsync(_dbContext, id, userId);

        ///<summary>
        /// Asynchronously finds a Vehicle entity by its identifier and user ID.
        /// </summary>
        /// <param name="id">The ID of the RailLine entity to find.</param>
        /// <param name="userId">The ID of the user who owns the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Vehicle entity if found; otherwise, null.</returns>
        private async Task<RailVehicle?> FindEntityByIdAsync(Guid id, string userId)
        {
            return await _dbContext.RailVehicles
                .Include(v => v.TractionDiagram)
                .FirstOrDefaultAsync(v => v.Id == id && v.UserId == userId && !v.IsDeleted);
        }
    }
}