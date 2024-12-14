using Application.Common;
using Application.GenericRepositories;
using AutoMapper;
using Domain.Common;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.GenericRepositories
{
    /// <inheritdoc cref="ISimpleModelWithUserRepository{TInputModel, TOutputModel}"/>
    public class SimpleModelWithUserRepository<TEntity, TInputModel, TOutputModel> : ISimpleModelWithUserRepository<TInputModel, TOutputModel>
        where TEntity : EntityWithUserBase
        where TInputModel : ModelBase
        where TOutputModel : ModelBase
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly IMapper _mapper;

        protected readonly DbSet<TEntity> _entities;

        public SimpleModelWithUserRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

            _entities = _dbContext.Set<TEntity>();
        }

        /// <inheritdoc cref="IGetOneWithUser{TOutputModel}.GetOneAsync(Guid, string)"/>
        public virtual async Task<TOutputModel?> GetOneAsync(Guid id, string userId)
            => _mapper.Map<TOutputModel>(await FindEntityAsync(id, userId));

        /// <inheritdoc cref="ICreateWithUser{TInputModel}.CreateAsync(TInputModel, string)"/>
        public virtual async Task CreateAsync(TInputModel model, string userId)
        {
            TEntity entity = _mapper.Map<TEntity>(model);
            entity.UserId = userId;
            entity.SetCreateHistory(userId);

            _entities.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc cref="IUpdateWithUser{TInputModel}.UpdateAsync(Guid, TInputModel, string)"/>
        public virtual async Task UpdateAsync(Guid id, TInputModel model, string userId)
        {
            TEntity? entity = await FindEntityAsync(id, userId);
            if (entity is null)
                return;

            _mapper.Map(model, entity);
            entity.SetUpdateHistory(userId);

            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc cref="IDeleteWithUser.DeleteAsync(Guid, string)"/>
        public virtual async Task SoftDeleteAsync(Guid id, string userId)
        {
            TEntity? entity = await FindEntityAsync(id, userId);
            if (entity is null)
                return;

            _entities.SoftOrHardDelete(entity, true, userId);
            await _dbContext.SaveChangesAsync();
        }

        protected virtual async Task<TEntity?> FindEntityAsync(Guid id, string userId)
            => await _entities.FindActiveEntityByPredicate(e => e.Id == id && e.UserId == userId);
    }

    /// <inheritdoc cref="ISimpleModelWithUserRepository{TModel}"/>
    public class SimpleModelWithUserRepository<TEntity, TModel>(
        ApplicationDbContext dbContext,
        IMapper mapper)
        : SimpleModelWithUserRepository<TEntity, TModel, TModel>(dbContext, mapper), ISimpleModelWithUserRepository<TModel>
        where TEntity : EntityWithUserBase
        where TModel : ModelBase
    {
    }
}
