using Application.Common;
using Domain.Entities;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a group of rail vehicles that are part of a train. If they use their tractive effort, they are active. Serves as an output model to be retrieved from the server.
    /// </summary>
    public class TrainVehicleOutputModel : ModelBase
    {
        public Guid VehicleId { get; set; }

        /// <summary>
        /// The name of the vehicle.
        /// </summary>
        public required string VehicleName { get; set; }

        /// <inheritdoc cref="TrainVehicle.VehicleCount"/>/>
        public short VehicleCount { get; set; }

        /// <inheritdoc cref="TrainVehicle.Position"/>
        public short Position { get; set; }

        /// <inheritdoc cref="TrainVehicle.IsActive"/>
        public bool IsActive { get; set; }
    }
}
