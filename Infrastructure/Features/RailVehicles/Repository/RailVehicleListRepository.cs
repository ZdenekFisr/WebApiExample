using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Domain.Entities;
using Infrastructure.DatabaseOperations.SoftDelete;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <inheritdoc cref="IRailVehicleListRepository"/>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="softDeleteOperation">The operation to perform soft delete on an entity.</param>
    public class RailVehicleListRepository(
        ApplicationDbContext dbContext,
        ISoftDeleteOperation softDeleteOperation)
        : IRailVehicleListRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly ISoftDeleteOperation _softDeleteOperation = softDeleteOperation;

        /// <inheritdoc />
        public async Task<ICollection<RailVehicleDrivingListModel>> GetDrivingVehiclesAsync(string userId)
            => await _dbContext.RailVehicles
                .AsNoTracking()
                .Include(v => v.TractionSystems)
                .Where(v => v.UserId == userId)
                .Where(v => !v.IsDeleted)
                .Where(v => v.TractionSystems.Count != 0)
                .Select(v => RailVehicleDrivingListModel.FromEntity(v))
                .ToListAsync();

        /// <inheritdoc />
        public async Task<ICollection<RailVehiclePulledListModel>> GetPulledVehiclesAsync(string userId)
            => await _dbContext.RailVehicles
                .AsNoTracking()
                .Include(v => v.TractionSystems)
                .Where(v => v.UserId == userId)
                .Where(v => !v.IsDeleted)
                .Where(v => v.TractionSystems.Count == 0)
                .Select(v => RailVehiclePulledListModel.FromEntity(v))
                .ToListAsync();

        /// <inheritdoc />
        public async Task SoftDeleteAsync(Guid id, string userId)
            => await _softDeleteOperation.SoftDeleteAsync<RailVehicle>(_dbContext, id, userId);
    }
}
