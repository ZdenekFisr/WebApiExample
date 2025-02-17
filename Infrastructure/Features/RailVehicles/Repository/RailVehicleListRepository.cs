using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DatabaseOperations.SoftDelete;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <inheritdoc cref="IRailVehicleListRepository"/>
    /// <param name="mapper">The mapper to map between model and entity.</param>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="softDeleteOperation">The operation to perform soft delete on an entity.</param>
    public class RailVehicleListRepository(
        IMapper mapper,
        ApplicationDbContext dbContext,
        ISoftDeleteOperation softDeleteOperation)
        : IRailVehicleListRepository
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly ISoftDeleteOperation _softDeleteOperation = softDeleteOperation;

        /// <inheritdoc />
        public async Task<ICollection<RailVehicleDrivingListModel>> GetDrivingVehiclesAsync(string userId)
        {
            return _mapper.Map<ICollection<RailVehicleDrivingListModel>>(
                await _dbContext.RailVehicles
                .AsNoTracking()
                .Include(v => v.TractionSystems)
                .Where(v => v.UserId == userId)
                .Where(v => !v.IsDeleted)
                .Where(v => v.TractionSystems.Count != 0)
                .ToListAsync());
        }

        /// <inheritdoc />
        public async Task<ICollection<RailVehiclePulledListModel>> GetPulledVehiclesAsync(string userId)
        {
            return _mapper.Map<ICollection<RailVehiclePulledListModel>>(
                await _dbContext.RailVehicles
                .AsNoTracking()
                .Include(v => v.TractionSystems)
                .Where(v => v.UserId == userId)
                .Where(v => !v.IsDeleted)
                .Where(v => v.TractionSystems.Count == 0)
                .ToListAsync());
        }

        /// <inheritdoc />
        public async Task SoftDeleteAsync(Guid id, string userId)
            => await _softDeleteOperation.SoftDeleteAsync<RailVehicle>(_dbContext, id, userId);
    }
}
