using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;

        protected readonly DbSet<TEntity> _entities;

        public SimpleModelRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;

            _entities = _context.Set<TEntity>();
        }

        /// <inheritdoc cref="IGetOne{TModel}"/>
        public virtual async Task<TModel?> GetOneAsync(Guid id)
            => _mapper.Map<TModel>(await FindEntity(id));

        /// <inheritdoc cref="ICreate{TModel}"/>
        public virtual async Task CreateAsync(TModel model)
        {
            TEntity entity = _mapper.Map<TEntity>(model);
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc cref="IUpdate{TModel}"/>
        public virtual async Task UpdateAsync(Guid id, TModel model)
        {
            TEntity entity = _mapper.Map<TEntity>(model);
            entity.Id = id;
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc cref="IDelete"/>
        public virtual async Task DeleteAsync(Guid id)
        {
            TEntity? entity = await FindEntity(id);
            if (entity is not null)
            {
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        protected virtual async Task<TEntity?> FindEntity(Guid id)
            => await _entities.FirstOrDefaultAsync(x => x.Id == id);
    }
}
