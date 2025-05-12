using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Application.Services;
using Domain.Entities;
using Infrastructure.DatabaseOperations.Insert;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <inheritdoc cref="IRailVehicleRepository{TModel}"/>
    /// <param name="mapper">The mapper to map between model and entity.</param>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="insertOperation">The operation to insert a row into the database.</param>
    /// <param name="updateOperation">The operation to update a row in the database.</param>
    public class RailVehicleRepository(
        ApplicationDbContext dbContext,
        IInsertOperation insertOperation,
        ICurrentUtcTimeProvider timeProvider)
        : IRailVehicleRepository<RailVehicleModelBase>
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IInsertOperation _insertOperation = insertOperation;
        private readonly ICurrentUtcTimeProvider _timeProvider = timeProvider;

        /// <inheritdoc />
        public async Task<RailVehicleModelBase?> GetOneAsync(Guid id, string userId)
        {
            RailVehicle? entity = await _dbContext.RailVehicles
                .AsNoTracking()
                .Include(v => v.TractionSystems)
                .ThenInclude(vts => vts.TractionDiagram)
                .FirstOrDefaultAsync(v => v.Id == id && v.UserId == userId && !v.IsDeleted); ;
            if (entity is null)
                return null;

            if (entity.TractionSystems.Count == 0)
                return RailVehiclePulledModel.FromEntity(entity);

            return RailVehicleDrivingModel.FromEntity(entity);
        }

        /// <inheritdoc />
        public async Task CreateAsync(RailVehicleModelBase model, string userId)
            => await _insertOperation.InsertAsync(_dbContext, userId, model.ToEntity);

        /// <inheritdoc />
        public async Task UpdateAsync(Guid id, RailVehicleModelBase newModel, string userId)
        {
            RailVehicle? entity = await FindEntityByIdAsync(id, userId);
            if (entity is null)
                return;

            entity.Name = newModel.Name;
            entity.Description = newModel.Description;
            entity.Weight = newModel.Weight;
            entity.Length = newModel.Length;
            entity.Wheelsets = newModel.Wheelsets;
            entity.EquivalentRotatingWeight = newModel.EquivalentRotatingWeight;
            entity.MaxSpeed = newModel.MaxSpeed;
            entity.ResistanceConstant = newModel.ResistanceConstant;
            entity.ResistanceLinear = newModel.ResistanceLinear;
            entity.ResistanceQuadratic = newModel.ResistanceQuadratic;

            entity.TractionSystems.Clear();
            if (newModel is RailVehicleDrivingModel drivingModel)
            {
                foreach (var tractionSystem in drivingModel.TractionSystems)
                {
                    entity.TractionSystems.Add(tractionSystem.ToEntity());
                }
            }

            entity.UpdatedAt = _timeProvider.GetCurrentUtcTime();
            entity.UpdatedBy = userId;

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously finds a rail vehicle entity by its identifier and user ID.
        /// </summary>
        /// <param name="id">The ID of the entity to find.</param>
        /// <param name="userId">The ID of the user who owns the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
        private async Task<RailVehicle?> FindEntityByIdAsync(Guid id, string userId)
        {
            return await _dbContext.RailVehicles
                .Include(v => v.TractionSystems)
                .ThenInclude(vts => vts.TractionDiagram)
                .FirstOrDefaultAsync(v => v.Id == id && v.UserId == userId && !v.IsDeleted);
        }
    }
}
