using Application.Features.RailVehicles.ListModel;
using Application.Features.RailVehicles.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for listing rail vehicles.
    /// </summary>
    /// <param name="mapper">The mapper to map between entities and DTOs.</param>
    /// <param name="dbContext">The application's database context.</param>
    public class RailVehicleListRepository(
        IMapper mapper,
        ApplicationDbContext dbContext)
        : IRailVehicleListRepository<RailVehicleListModel>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<ICollection<RailVehicleListModel>> GetManyAsync(string userId)
        {
            return _mapper.Map<ICollection<RailVehicleListModel>>(
                await _dbContext.RailVehicles
                .AsNoTracking()
                .Where(v => v.UserId == userId)
                .Where(v => !v.IsDeleted)
                .ToListAsync());
        }
    }
}
