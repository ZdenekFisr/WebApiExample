using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Application.Services;
using Domain.Entities;
using Infrastructure.DatabaseOperations.Insert;
using Infrastructure.Exceptions;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <inheritdoc cref="ITrainRepository{TInputModel, TOutputModel}"/>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="vehicleNameRepository">The repository for vehicle names.</param>
    /// <param name="insertRowOperation">The operation for inserting a row into the database.</param>
    /// <param name="updateOperation">The operation for updating entity information.</param>
    public class TrainRepository(
        ApplicationDbContext dbContext,
        IRailVehicleNameRepository vehicleNameRepository,
        IInsertOperation insertRowOperation,
        ICurrentUtcTimeProvider timeProvider)
        : ITrainRepository<TrainInputModel, TrainOutputModel>
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IRailVehicleNameRepository _vehicleNameRepository = vehicleNameRepository;
        private readonly IInsertOperation _insertRowOperation = insertRowOperation;
        private readonly ICurrentUtcTimeProvider _timeProvider = timeProvider;

        /// <inheritdoc />
        public async Task<TrainOutputModel?> GetOneAsync(Guid id, string userId)
        {
            Train? entity = await _dbContext.Trains
                .AsNoTracking()
                .Include(t => t.TrainVehicles
                    .Where(tv => _dbContext.RailVehicles.Any(v => v.Id == tv.VehicleId && v.UserId == userId && !v.IsDeleted))
                    .OrderBy(tv => tv.Position))
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId && !t.IsDeleted);

            if (entity is null)
                return null;

            TrainOutputModel model = TrainOutputModel.FromEntity(entity);

            Dictionary<Guid, string> vehicleNames = await _vehicleNameRepository.GetVehicleNames(userId);
            if (model.TrainVehicles is not null)
            {
                foreach (TrainVehicleOutputModel trainVehicle in model.TrainVehicles)
                {
                    trainVehicle.VehicleName = vehicleNames[trainVehicle.VehicleId];
                }
            }

            return model;
        }

        /// <inheritdoc />
        /// <exception cref="VehicleForeignKeyException">Thrown when some rail vehicle IDs are not present in the database.</exception>
        public async Task CreateAsync(TrainInputModel model, string userId)
        {
            try
            {
                await _insertRowOperation.InsertAsync(_dbContext, userId, model.ToEntity);
            }
            catch (DbUpdateException ex)
            {
                if (ex.IsForeignKeyViolation())
                    throw new VehicleForeignKeyException(ex, await FindMissingVehicleIds(model, userId));
                else
                    throw;
            }
        }

        /// <inheritdoc />
        /// <exception cref="VehicleForeignKeyException">Thrown when some rail vehicle IDs are not present in the database.</exception>
        public async Task UpdateAsync(Guid id, TrainInputModel newModel, string userId)
        {
            Train? entity = await FindEntityByIdAsync(id, userId);
            if (entity is null)
                return;

            entity.Name = newModel.Name;
            entity.Description = newModel.Description;
            entity.MaxPullForce = newModel.MaxPullForce;

            entity.TrainVehicles.Clear();
            foreach (TrainVehicleInputModel trainVehicle in newModel.TrainVehicles)
            {
                entity.TrainVehicles.Add(trainVehicle.ToEntity());
            }

            entity.UpdatedAt = _timeProvider.GetCurrentUtcTime();
            entity.UpdatedBy = userId;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.IsForeignKeyViolation())
                    throw new VehicleForeignKeyException(ex, await FindMissingVehicleIds(newModel, userId));
                else
                    throw;
            }
        }

        /// <summary>
        /// Asynchronously finds a train entity by its identifier and user ID.
        /// </summary>
        /// <param name="id">The ID of the entity to find.</param>
        /// <param name="userId">The ID of the user who owns the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
        private async Task<Train?> FindEntityByIdAsync(Guid id, string userId)
            => await _dbContext.Trains
                .Include(t => t.TrainVehicles)
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        /// <summary>
        /// Asynchronously finds missing rail vehicle IDs from a given model.
        /// </summary>
        /// <param name="model">The model with rail vehicle IDs.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>Rail vehicle IDs as strings.</returns>
        private async Task<string[]> FindMissingVehicleIds(TrainInputModel model, string userId)
        {
            var vehicleIdsInModel = model.TrainVehicles.Select(tv => tv.VehicleId).ToList();
            var vehicleIdsInDb = await _dbContext.RailVehicles
                .Where(v => vehicleIdsInModel.Contains(v.Id) && v.UserId == userId && !v.IsDeleted)
                .Select(v => v.Id)
                .ToListAsync();

            string[] missingVehicleIds = [.. vehicleIdsInModel
                .Except(vehicleIdsInDb)
                .Select(id => id.ToString())];

            return missingVehicleIds;
        }
    }
}
