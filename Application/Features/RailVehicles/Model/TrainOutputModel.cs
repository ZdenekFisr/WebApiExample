using Application.Common;
using Domain.Entities;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a train that consists of multiple rail vehicles. Serves as an output model to be retrieved from the server.
    /// </summary>
    public class TrainOutputModel : ModelBase
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        /// <inheritdoc cref="Train.MaxPullForce"/>
        public short MaxPullForce { get; set; }

        /// <inheritdoc cref="Train.TrainVehicles"/>
        public required ICollection<TrainVehicleOutputModel> TrainVehicles { get; set; }
    }
}
