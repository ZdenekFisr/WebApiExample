using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DatabaseOperations.HardDelete;
using Infrastructure.DatabaseOperations.Insert;
using Infrastructure.DatabaseOperations.Update;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Infrastructure.Features.RailVehicles.Repository
{
    /// <inheritdoc cref="IElectrificationTypeRepository{TModel, TListModel}"/>
    /// <param name="mapper">The mapper to map between model and entity.</param>
    /// <param name="dbContext">The application's database context.</param>
    /// <param name="insertOperation">The operation to insert a row into the database.</param>
    /// <param name="updateOperation">The operation to update a row in the database.</param>
    /// <param name="hardDeleteOperation">The operation to delete a row from the database.</param>
    public class ElectrificationTypeRepository(
        IMapper mapper,
        ApplicationDbContext dbContext,
        IInsertOperation insertOperation,
        IUpdateOperation updateOperation,
        IHardDeleteOperation hardDeleteOperation)
        : IElectrificationTypeRepository<ElectrificationTypeModel, ElectrificationTypeListModel>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IInsertOperation _insertOperation = insertOperation;
        private readonly IUpdateOperation _updateOperation = updateOperation;
        private readonly IHardDeleteOperation _hardDeleteOperation = hardDeleteOperation;

        /// <inheritdoc />
        public async Task<ICollection<ElectrificationTypeListModel>> GetManyAsync(string userId)
            => _mapper.Map<ICollection<ElectrificationTypeListModel>>(await _dbContext.ElectrificationTypes
                .AsNoTracking()
                .Where(e => e.UserId == userId)
                .Where(e => !e.IsDeleted)
                .ToArrayAsync());

        /// <inheritdoc />
        public async Task CreateAsync(ElectrificationTypeModel model, string userId)
            => await _insertOperation.InsertAsync<ElectrificationType, ElectrificationTypeModel>(_dbContext, model, userId);

        /// <inheritdoc />
        public async Task UpdateAsync(Guid id, ElectrificationTypeModel newModel, string userId)
            => await _updateOperation.UpdateAsync(_dbContext, FindEntityByIdAsync, id, newModel, userId);

        /// <inheritdoc />
        /// <exception cref="ElectrificationTypeForeignKeyException">Thrown when the entity is referenced by other entities.</exception>
        public async Task HardDeleteAsync(Guid id, string userId)
        {
            try
            {
                await _hardDeleteOperation.HardDeleteAsync<ElectrificationType>(_dbContext, id, userId);
            }
            catch (DbUpdateException ex)
            {
                string[] relatedVehicles = await _dbContext.RailVehicles
                    .Where(v => v.TractionSystems.Any(vts => vts.ElectrificationTypeId == id))
                    .Select(v => new string(v.Name))
                    .ToArrayAsync();

                StringBuilder errorResponse = new();
                errorResponse.AppendLine("Cannot delete ElectrificationType because it is referenced by other entities.");

                if (relatedVehicles.Length != 0)
                {
                    errorResponse.AppendLine("Related vehicles:");
                    foreach (string vehicle in relatedVehicles)
                    {
                        errorResponse.AppendLine($"-- {vehicle}");
                    }
                }

                throw new ElectrificationTypeForeignKeyException(ex, errorResponse.ToString());
            }
        }

        /// <summary>
        /// Asynchronously finds an electrification type entity by its identifier and user ID.
        /// </summary>
        /// <param name="id">The ID of the entity to find.</param>
        /// <param name="userId">The ID of the user who owns the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
        private async Task<ElectrificationType?> FindEntityByIdAsync(Guid id, string userId)
        {
            return await _dbContext.ElectrificationTypes
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId && !e.IsDeleted);
        }
    }
}
