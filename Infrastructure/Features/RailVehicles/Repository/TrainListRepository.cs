using Application.Features.RailVehicles.Extensions;
using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Domain.Entities;
using Infrastructure.DatabaseOperations.SoftDelete;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <inheritdoc cref="ITrainListRepository{TModel}"/>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="vehicleNameRepository">The repository for getting vehicle names.</param>
    /// <param name="softDeleteOperation">The operation for soft deleting an entity.</param>
    public class TrainListRepository(
        ApplicationDbContext dbContext,
        IRailVehicleNameRepository vehicleNameRepository,
        ISoftDeleteOperation softDeleteOperation)
        : ITrainListRepository<TrainListModel>
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IRailVehicleNameRepository _vehicleNameRepository = vehicleNameRepository;
        private readonly ISoftDeleteOperation _softDeleteOperation = softDeleteOperation;

        /// <inheritdoc />
        public async Task<ICollection<TrainListModel>> GetManyAsync(string userId)
        {
            TrainListModel[] trains = await _dbContext.Trains
                .AsNoTracking()
                .Where(t => t.UserId == userId)
                .Where(t => !t.IsDeleted)
                .Include(t => t.TrainVehicles
                    .Where(tv => _dbContext.RailVehicles.Any(v => v.Id == tv.VehicleId && v.UserId == userId && !v.IsDeleted))
                    .OrderBy(tv => tv.Position))
                .OrderBy(t => t.Name)
                .Select(t => TrainListModel.FromEntity(t))
                .ToArrayAsync();

            if (trains.Length == 0)
                return trains;

            Dictionary<Guid, string> vehicleNames = await _vehicleNameRepository.GetVehicleNames(userId);
            foreach (TrainListModel train in trains)
            {
                if (train.TrainVehicles is not null)
                {
                    foreach (TrainVehicleOutputModel trainVehicle in train.TrainVehicles)
                    {
                        trainVehicle.VehicleName = vehicleNames[trainVehicle.VehicleId];
                    }
                }
                train.Arrangement = train.GetArrangement();
            }

            return trains;
        }

        /// <inheritdoc />
        public async Task SoftDeleteAsync(Guid id, string userId)
            => await _softDeleteOperation.SoftDeleteAsync<Train>(_dbContext, id, userId);
    }
}
