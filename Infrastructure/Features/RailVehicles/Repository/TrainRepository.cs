using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DatabaseOperations.Insert;
using Infrastructure.DatabaseOperations.Update;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <inheritdoc cref="ITrainRepository{TInputModel, TOutputModel}"/>
    /// <param name="mapper">The mapper instance for object-object mapping.</param>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="vehicleNameRepository">The repository for vehicle names.</param>
    /// <param name="insertRowOperation">The operation for inserting a row into the database.</param>
    /// <param name="updateOperation">The operation for updating entity information.</param>
    public class TrainRepository(
        IMapper mapper,
        ApplicationDbContext dbContext,
        IRailVehicleNameRepository vehicleNameRepository,
        IInsertOperation insertRowOperation,
        IUpdateOperation updateOperation)
        : ITrainRepository<TrainInputModel, TrainOutputModel>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IRailVehicleNameRepository _vehicleNameRepository = vehicleNameRepository;
        private readonly IInsertOperation _insertRowOperation = insertRowOperation;
        private readonly IUpdateOperation _updateOperation = updateOperation;

        /// <inheritdoc />
        public async Task<TrainOutputModel?> GetOneAsync(Guid id, string userId)
        {
            TrainOutputModel? train = _mapper.Map<TrainOutputModel?>(await _dbContext.Trains
                .AsNoTracking()
                .Include(t => t.TrainVehicles
                    .Where(tv => _dbContext.RailVehicles.Any(v => v.Id == tv.VehicleId && v.UserId == userId && !v.IsDeleted))
                    .OrderBy(tv => tv.Position))
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId && !t.IsDeleted));

            if (train is null)
                return null;

            Dictionary<Guid, string> vehicleNames = await _vehicleNameRepository.GetVehicleNames(userId);
            if (train.TrainVehicles is not null)
            {
                foreach (TrainVehicleOutputModel trainVehicle in train.TrainVehicles)
                {
                    trainVehicle.VehicleName = vehicleNames[trainVehicle.VehicleId];
                }
            }

            return train;
        }

        /// <inheritdoc />
        /// <exception cref="VehicleForeignKeyException">Thrown when some rail vehicle IDs are not present in the database.</exception>
        public async Task CreateAsync(TrainInputModel model, string userId)
        {
            try
            {
                await _insertRowOperation.InsertAsync<Train, TrainInputModel>(_dbContext, model, userId);
            }
            catch (DbUpdateException ex)
            {
                throw new VehicleForeignKeyException(ex, await FindMissingVehicleIds(model, userId));
            }
        }

        /// <inheritdoc />
        /// <exception cref="VehicleForeignKeyException">Thrown when some rail vehicle IDs are not present in the database.</exception>
        public async Task UpdateAsync(Guid id, TrainInputModel newModel, string userId)
        {
            try
            {
                await _updateOperation.UpdateAsync(_dbContext, FindEntityByIdAsync, id, newModel, userId);
            }
            catch (DbUpdateException ex)
            {
                throw new VehicleForeignKeyException(ex, await FindMissingVehicleIds(newModel, userId));
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

            string[] missingVehicleIds = vehicleIdsInModel
                .Except(vehicleIdsInDb)
                .Select(id => id.ToString())
                .ToArray();

            return missingVehicleIds;
        }
    }
}
