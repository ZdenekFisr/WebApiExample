using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApiExample.GenericRepositories
{
    /// <inheritdoc cref="IAllSimpleModelsRepository{TModel}"/>
    public class AllSimpleModelsRepository<TEntity, TModel>(
        ApplicationDbContext dbContext,
        IMapper mapper)
        : SimpleModelRepository<TEntity, TModel>(dbContext, mapper), IAllSimpleModelsRepository<TModel>
        where TEntity : Entity
        where TModel : Model
    {
        /// <inheritdoc />
        public virtual async Task<List<TModel>> GetAllAsync()
            => _mapper.Map<List<TModel>>(await _entities.ToListAsync());
    }
}
