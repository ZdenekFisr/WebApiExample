using Application.Features.RailVehicles.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <inheritdoc cref="IRailVehicleNameRepository"/>
    /// <param name="dbContext">The application's database context.</param>
    public class RailVehicleNameRepository(
        ApplicationDbContext dbContext)
        : IRailVehicleNameRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<Dictionary<Guid, string>> GetVehicleNames(string userId)
        {
            return await _dbContext.RailVehicles
                .Where(v => v.UserId == userId)
                .Where(v => !v.IsDeleted)
                .ToDictionaryAsync(v => v.Id, v => v.Name);
        }
    }
}
