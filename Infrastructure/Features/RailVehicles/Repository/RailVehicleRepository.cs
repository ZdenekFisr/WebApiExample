using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DatabaseOperations.Insert;
using Infrastructure.DatabaseOperations.Update;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <inheritdoc cref="IRailVehicleRepository{TModel}"/>
    /// <param name="mapper">The mapper to map between model and entity.</param>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="insertOperation">The operation to insert a row into the database.</param>
    /// <param name="updateOperation">The operation to update a row in the database.</param>
    public class RailVehicleRepository(
        IMapper mapper,
        ApplicationDbContext dbContext,
        IInsertOperation insertOperation,
        IUpdateOperation updateOperation)
        : IRailVehicleRepository<RailVehicleModelBase>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IInsertOperation _insertOperation = insertOperation;
        private readonly IUpdateOperation _updateOperation = updateOperation;

        /// <inheritdoc />
        public async Task<RailVehicleModelBase?> GetOneAsync(Guid id, string userId)
        {
            RailVehicle? entity = await _dbContext.RailVehicles
                .AsNoTracking()
                .Include(v => v.TractionSystems)
                .ThenInclude(vts => vts.TractionDiagram)
                .FirstOrDefaultAsync(v => v.Id == id && v.UserId == userId && !v.IsDeleted); ;
            if (entity is null)
                return null;

            if (entity.TractionSystems.Count == 0)
                return _mapper.Map<RailVehiclePulledModel>(entity);

            return _mapper.Map<RailVehicleDrivingModel>(entity);
        }

        /// <inheritdoc />
        public async Task CreateAsync(RailVehicleModelBase model, string userId)
            => await _insertOperation.InsertAsync<RailVehicle, RailVehicleModelBase>(_dbContext, model, userId);

        /// <inheritdoc />
        public async Task UpdateAsync(Guid id, RailVehicleModelBase newmodel, string userId)
            => await _updateOperation.UpdateAsync(_dbContext, FindEntityByIdAsync, id, newmodel, userId);

        /// <summary>
        /// Asynchronously finds a rail vehicle entity by its identifier and user ID.
        /// </summary>
        /// <param name="id">The ID of the entity to find.</param>
        /// <param name="userId">The ID of the user who owns the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
        private async Task<RailVehicle?> FindEntityByIdAsync(Guid id, string userId)
        {
            return await _dbContext.RailVehicles
                .Include(v => v.TractionSystems)
                .ThenInclude(vts => vts.TractionDiagram)
                .FirstOrDefaultAsync(v => v.Id == id && v.UserId == userId && !v.IsDeleted);
        }
    }
}
