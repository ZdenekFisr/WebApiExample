using Application.Common;
using Application.GenericRepositories;
using AutoMapper;
using Domain.Common;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.GenericRepositories
{
    /// <inheritdoc cref="ISimpleModelRepository{TInputModel, TOutputModel}"/>
    public class SimpleModelRepository<TEntity, TInputModel, TOutputModel> : ISimpleModelRepository<TInputModel, TOutputModel>
        where TEntity : EntityBase
        where TInputModel : ModelBase
        where TOutputModel : ModelBase
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly IMapper _mapper;

        protected readonly DbSet<TEntity> _entities;

        public SimpleModelRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

            _entities = _dbContext.Set<TEntity>();
        }

        /// <inheritdoc cref="IGetOne{TOutputModel}.GetOneAsync(Guid)"/>
        public virtual async Task<TOutputModel?> GetOneAsync(Guid id)
            => _mapper.Map<TOutputModel>(await FindEntityAsync(id));

        /// <inheritdoc cref="ICreate{TInputModel}.CreateAsync(TInputModel)"/>
        public virtual async Task CreateAsync(TInputModel model)
        {
            TEntity entity = _mapper.Map<TEntity>(model);
            entity.SetCreateHistory();

            _entities.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc cref="IUpdate{TInputModel}.UpdateAsync(Guid, TInputModel)"/>
        public virtual async Task UpdateAsync(Guid id, TInputModel model)
        {
            TEntity? entity = await FindEntityAsync(id);
            if (entity is null)
                return;

            _mapper.Map(model, entity);
            entity.SetUpdateHistory();

            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc cref="IDelete.DeleteAsync(Guid)"/>
        public virtual async Task DeleteAsync(Guid id)
        {
            TEntity? entity = await FindEntityAsync(id);
            if (entity is null)
                return;

            _entities.SoftOrHardDelete(entity, true);
            await _dbContext.SaveChangesAsync();
        }

        protected virtual async Task<TEntity?> FindEntityAsync(Guid id)
            => await _entities.FindActiveEntityByPredicate(e => e.Id == id);
    }

    /// <inheritdoc cref="ISimpleModelRepository{TModel}"/>
    public class SimpleModelRepository<TEntity, TModel>(
        ApplicationDbContext dbContext,
        IMapper mapper)
        : SimpleModelRepository<TEntity, TModel, TModel>(dbContext, mapper), ISimpleModelRepository<TModel>
        where TEntity : EntityBase
        where TModel : ModelBase
    {
    }
}
