using Application.Features.RailVehicles.ListModel;
using Application.Features.RailVehicles.Repository;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DatabaseOperations.Restore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for listing and restoring deleted rail vehicles.
    /// </summary>
    /// <param name="mapper">The mapper to map between model and entity.</param>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="restoreOperation">The operation to restore an entity.</param>
    public class RailVehicleDeletedRepository(
        IMapper mapper,
        ApplicationDbContext dbContext,
        IRestoreOperation restoreOperation)
        : IRailVehicleDeletedRepository<RailVehicleListModel>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IRestoreOperation _restoreOperation = restoreOperation;

        /// <inheritdoc />
        public async Task<ICollection<RailVehicleListModel>> GetManyAsync(string userId)
        {
            return _mapper.Map<ICollection<RailVehicleListModel>>(
                await _dbContext.RailVehicles
                .AsNoTracking()
                .Where(v => v.UserId == userId)
                .Where(v => v.IsDeleted)
                .ToListAsync());
        }

        /// <inheritdoc />
        public async Task RestoreAsync(Guid id, string userId)
            => await _restoreOperation.RestoreAsync<RailVehicle>(_dbContext, id, userId);
    }
}
