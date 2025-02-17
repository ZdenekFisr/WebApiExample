using Application.Common;
using Application.Services;
using AutoMapper;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Update
{
    /// <summary>
    /// Service for performing update operations on entities.
    /// </summary>
    /// <param name="mapper">The mapper to map between model and entity.</param>
    /// <param name="currentUtcTimeProvider">The provider to get the current UTC time.</param>
    public class UpdateOperation(
        IMapper mapper,
        ICurrentUtcTimeProvider currentUtcTimeProvider)
        : IUpdateOperation
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider = currentUtcTimeProvider;

        /// <inheritdoc />
        public async Task UpdateAsync<TEntity, TModel>(DbContext dbContext, Func<Guid, string, Task<TEntity?>> findEntityMethod, Guid id, TModel newModel, string userId)
            where TEntity : EntityWithUserBase
            where TModel : ModelBase
        {
            TEntity? existingEntity = await findEntityMethod(id, userId);
            if (existingEntity is null)
                return;

            _mapper.Map(newModel, existingEntity);

            if (existingEntity is IUpdateHistory entityUpdateHistory)
            {
                entityUpdateHistory.UpdatedAt = _currentUtcTimeProvider.GetCurrentUtcTime();
                entityUpdateHistory.UpdatedBy = userId;
            }

            dbContext.Update(existingEntity);

            await dbContext.SaveChangesAsync();
        }
    }
}
