using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DatabaseOperations.HardDelete;
using Infrastructure.DatabaseOperations.Restore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <inheritdoc cref="ITrainDeletedRepository{TModel}"/>
    /// <param name="mapper">The AutoMapper instance for mapping entities and models.</param>
    /// <param name="dbContext">The application database context.</param>
    /// <param name="restoreOperation">The operation for restoring an entity.</param>
    /// <param name="hardDeleteOperation">The operation for hard deleting an entity.</param>
    public class TrainDeletedRepository(
        IMapper mapper,
        ApplicationDbContext dbContext,
        IRestoreOperation restoreOperation,
        IHardDeleteOperation hardDeleteOperation)
        : ITrainDeletedRepository<TrainDeletedModel>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IRestoreOperation _restoreOperation = restoreOperation;
        private readonly IHardDeleteOperation _hardDeleteOperation = hardDeleteOperation;

        /// <inheritdoc />
        public async Task<ICollection<TrainDeletedModel>> GetManyAsync(string userId)
        {
            return _mapper.Map<List<TrainDeletedModel>>(await _dbContext.Trains
                .AsNoTracking()
                .Where(l => l.UserId == userId)
                .Where(l => l.IsDeleted)
                .ToListAsync());
        }

        /// <inheritdoc />
        public async Task RestoreAsync(Guid id, string userId)
            => await _restoreOperation.RestoreAsync<Train>(_dbContext, id, userId);

        /// <inheritdoc />
        public async Task HardDeleteAsync(Guid id, string userId)
            => await _hardDeleteOperation.HardDeleteAsync<Train>(_dbContext, id, userId);
    }
}
