﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiExample.Extensions;
using WebApiExample.RepositoryInterfaces;

namespace WebApiExample.GenericRepositories.SimpleModel
{
    /// <summary>
    /// Contains CRUD operations for models mapped from database entities that have no foreign key to other database tables. It can be inherited by other services and its methods can be overridden to suit a more complex model.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TModel">Type of corresponding model that is mapped to the entity with AutoMapper and vice-versa.</typeparam>
    public class SimpleModelRepository<TEntity, TModel> : ISimpleModelRepository<TModel>
        where TEntity : Entity
        where TModel : Model
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

        /// <inheritdoc cref="IGetOne{TModel}.GetOneAsync(Guid)"/>
        public virtual async Task<TModel?> GetOneAsync(Guid id)
            => _mapper.Map<TModel>(await FindEntity(id));

        /// <inheritdoc cref="ICreate{TModel}.CreateAsync(TModel)"/>
        public virtual async Task CreateAsync(TModel model)
        {
            TEntity entity = _mapper.Map<TEntity>(model);
            entity.SetCreateHistory();

            _entities.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc cref="IUpdate{TModel}.UpdateAsync(Guid, TModel)"/>
        public virtual async Task UpdateAsync(Guid id, TModel model)
        {
            TEntity? entity = await FindEntity(id);
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
            TEntity? entity = await FindEntity(id);
            if (entity is null)
                return;

            _entities.SoftOrHardDelete(entity, true);
            await _dbContext.SaveChangesAsync();
        }

        protected virtual async Task<TEntity?> FindEntity(Guid id)
            => await _entities.FindActiveEntityByPredicate(e => e.Id == id);
    }
}
