using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Domain.Entities;
using Infrastructure.DatabaseOperations.HardDelete;
using Infrastructure.DatabaseOperations.Restore;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <inheritdoc cref="IRailVehicleDeletedRepository{TModel}"/>
    /// <param name="dbContext">The application database context.</param>
    /// <param name="restoreOperation">The operation for restoring an entity.</param>
    /// <param name="hardDeleteOperation">The operation for hard deleting an entity.</param>
    public class RailVehicleDeletedRepository(
        ApplicationDbContext dbContext,
        IRestoreOperation restoreOperation,
        IHardDeleteOperation hardDeleteOperation)
        : IRailVehicleDeletedRepository<RailVehicleDeletedModel>
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IRestoreOperation _restoreOperation = restoreOperation;
        private readonly IHardDeleteOperation _hardDeleteOperation = hardDeleteOperation;

        /// <inheritdoc />
        public async Task<ICollection<RailVehicleDeletedModel>> GetManyAsync(string userId)
            => await _dbContext.RailVehicles
                .AsNoTracking()
                .Where(v => v.UserId == userId)
                .Where(v => v.IsDeleted)
                .Select(v => RailVehicleDeletedModel.FromEntity(v))
                .ToListAsync();

        /// <inheritdoc />
        public async Task RestoreAsync(Guid id, string userId)
            => await _restoreOperation.RestoreAsync<RailVehicle>(_dbContext, id, userId);

        /// <inheritdoc />
        /// <exception cref="VehicleForeignKeyException">Thrown when the entity is referenced by other entities.</exception>
        public async Task HardDeleteAsync(Guid id, string userId)
        {
            try
            {
                await _hardDeleteOperation.HardDeleteAsync<RailVehicle>(_dbContext, id, userId);
            }
            catch (DbUpdateException ex)
            {
                string[] relatedTrains = await _dbContext.Trains
                    .Where(t => t.TrainVehicles.Any(tv => tv.VehicleId == id))
                    .Select(t => new string(t.Name))
                    .ToArrayAsync();

                StringBuilder errorResponse = new();
                errorResponse.AppendLine("Cannot delete Vehicle because it is referenced by other entities.");

                if (relatedTrains.Length != 0)
                {
                    errorResponse.AppendLine("Related trains:");
                    foreach (string train in relatedTrains)
                    {
                        errorResponse.AppendLine($"- {train}");
                    }
                }

                throw new VehicleForeignKeyException(ex, errorResponse.ToString());
            }
        }
    }
}
