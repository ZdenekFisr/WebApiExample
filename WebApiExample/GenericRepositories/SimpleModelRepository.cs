using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApiExample.GenericRepositories
{
    /// <inheritdoc cref="ISimpleModelRepository{TModel}"/>
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

        /// <inheritdoc />
        public virtual async Task<TModel?> GetOneAsync(Guid id)
            => _mapper.Map<TModel>(await _entities.FirstOrDefaultAsync(x => x.Id == id));

        /// <inheritdoc />
        public virtual async Task CreateAsync(TModel model)
        {
            TEntity entity = _mapper.Map<TEntity>(model);
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public virtual async Task UpdateAsync(Guid id, TModel model)
        {
            TEntity entity = _mapper.Map<TEntity>(model);
            entity.Id = id;
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public virtual async Task DeleteAsync(Guid id)
        {
            TEntity? entity = await _entities.FindAsync(id);
            if (entity is not null)
            {
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
