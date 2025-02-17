using Application.Common.RepositoryInterfaces;
using Application.Features.RailVehicles.Model;

namespace Application.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for listing rail vehicles.
    /// </summary>
    public interface IRailVehicleListRepository : ISoftDeleteWithUser
    {
        /// <summary>
        /// Gets all driving rail vehicles that are not deleted and are owned by the given user.
        /// </summary>
        /// <param name="userId">User's ID.</param>
        /// <returns>A collection of driving vehicles.</returns>
        Task<ICollection<RailVehicleDrivingListModel>> GetDrivingVehiclesAsync(string userId);

        /// <summary>
        /// Gets all pulled rail vehicles that are not deleted and are owned by the given user.
        /// </summary>
        /// <param name="userId">User's ID.</param>
        /// <returns>A collection of pulled vehicles.</returns>
        Task<ICollection<RailVehiclePulledListModel>> GetPulledVehiclesAsync(string userId);
    }
}
