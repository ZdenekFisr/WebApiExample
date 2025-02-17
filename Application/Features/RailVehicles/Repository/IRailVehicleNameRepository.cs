namespace Application.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for retrieving vehicle names.
    /// </summary>
    public interface IRailVehicleNameRepository
    {
        /// <summary>
        /// Gets the vehicle names for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a dictionary where the key is the vehicle ID and the value is the vehicle name.</returns>
        Task<Dictionary<Guid, string>> GetVehicleNames(string userId);
    }
}
