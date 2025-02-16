using Application.Features.RailVehicles.ListModel;
using Application.Features.RailVehicles.Repository;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DatabaseOperations.SoftDelete;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for listing rail vehicles.
    /// </summary>
    /// <param name="mapper">The mapper to map between model and entity.</param>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="softDeleteOperation">The operation to perform soft delete on an entity.</param>
    public class RailVehicleListRepository(
        IMapper mapper,
        ApplicationDbContext dbContext,
        ISoftDeleteOperation softDeleteOperation)
        : IRailVehicleListRepository<RailVehicleListModel>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly ISoftDeleteOperation _softDeleteOperation = softDeleteOperation;

        /// <inheritdoc />
        public async Task<ICollection<RailVehicleListModel>> GetManyAsync(string userId)
        {
            return _mapper.Map<ICollection<RailVehicleListModel>>(
                await _dbContext.RailVehicles
                .AsNoTracking()
                .Where(v => v.UserId == userId)
                .Where(v => !v.IsDeleted)
                .ToListAsync());
        }

        /// <inheritdoc />
        public async Task SoftDeleteAsync(Guid id, string userId)
            => await _softDeleteOperation.SoftDeleteAsync<RailVehicle>(_dbContext, id, userId);
    }
}
