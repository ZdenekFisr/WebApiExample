using Application.Common;
using AutoMapper;
using Domain.Common;
using Infrastructure.Services.CurrentUtcTime;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Insert
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InsertOperation{TEntity, TDto}"/> class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDto">The type of the data transfer object.</typeparam>
    /// <param name="mapper">The mapper to map between DTO and entity.</param>
    /// <param name="currentUtcTimeProvider">The provider to get the current UTC time.</param>
    public class InsertOperation<TEntity, TDto>(
        IMapper mapper,
        ICurrentUtcTimeProvider currentUtcTimeProvider)
        : IInsertOperation<TDto>
            where TEntity : EntityWithUserBase
            where TDto : ModelBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider = currentUtcTimeProvider;

        /// <inheritdoc />
        public async Task InsertAsync(DbContext dbContext, TDto dto, string userId)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            entity.UserId = userId;

            if (entity is ICreateHistory entityCreateHistory)
            {
                entityCreateHistory.CreatedAt = _currentUtcTimeProvider.GetCurrentUtcTime();
                entityCreateHistory.CreatedBy = userId;
            }

            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
