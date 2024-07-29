using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiExample.RepositoryInterfaces;

namespace WebApiExample.GenericRepositories.SimpleModelWithUser
{
    /// <summary>
    /// Contains CRUD operations for models mapped from database entities that have no foreign key to other database tables and are bound to users with user ID. It can be inherited by other services and its methods can be overridden to suit a more complex model.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TModel">Type of corresponding model that is mapped to the entity with AutoMapper and vice-versa.</typeparam>
    public class SimpleModelWithUserRepository<TEntity, TModel> : ISimpleModelWithUserRepository<TModel>
        where TEntity : EntityWithUser
        where TModel : Model
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;

        protected readonly DbSet<TEntity> _entities;

        public SimpleModelWithUserRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;

            _entities = _context.Set<TEntity>();
        }

        /// <inheritdoc cref="IGetOneWithUser{TModel}"/>
        public virtual async Task<TModel?> GetOneAsync(Guid id, string userId)
            => _mapper.Map<TModel>(await FindEntity(id, userId));

        /// <inheritdoc cref="ICreateWithUser{TModel}"/>
        public virtual async Task CreateAsync(TModel model, string userId)
        {
            TEntity entity = _mapper.Map<TEntity>(model);
            entity.UserId = userId;
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc cref="IUpdateWithUser{TModel}"/>
        public virtual async Task UpdateAsync(Guid id, TModel model, string userId)
        {
            TEntity entity = _mapper.Map<TEntity>(model);
            entity.Id = id;
            entity.UserId = userId;
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc cref="IDelete"/>
        public virtual async Task DeleteAsync(Guid id, string userId)
        {
            TEntity? entity = await FindEntity(id, userId);
            if (entity is not null)
            {
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        protected virtual async Task<TEntity?> FindEntity(Guid id, string userId)
            => await _entities.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
    }
}
