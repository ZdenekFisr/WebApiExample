using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DatabaseOperations.Insert;
using Infrastructure.DatabaseOperations.Update;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository class for managing Vehicle entities.
    /// </summary>
    /// <param name="mapper">The mapper to map between model and entity.</param>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="insertRowOperation">The operation to insert a row into the database.</param>
    /// <param name="updateOperation">The operation to update an entity.</param>
    public class RailVehicleRepository(
        IMapper mapper,
        ApplicationDbContext dbContext,
        IInsertOperation insertRowOperation,
        IUpdateOperation updateOperation)
        : IRailVehicleRepository<RailVehicleModelBase>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IInsertOperation _insertRowOperation = insertRowOperation;
        private readonly IUpdateOperation _updateOperation = updateOperation;

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
        public async Task CreateAsync(RailVehicleModelBase model, string userId)
            => await _insertRowOperation.InsertAsync<RailVehicle, RailVehicleModelBase>(_dbContext, model, userId);

        /// <inheritdoc />
        public async Task UpdateAsync(Guid id, RailVehicleModelBase newmodel, string userId)
            => await _updateOperation.UpdateAsync(_dbContext, FindEntityByIdAsync, id, newmodel, userId);

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
