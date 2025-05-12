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

        /// <summary>
        /// Creates a new instance of <see cref="TrainVehicleOutputModel"/> from a <see cref="TrainVehicle"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="TrainVehicle"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="TrainVehicleOutputModel"/>.</returns>
        public static TrainVehicleOutputModel FromEntity(TrainVehicle entity)
        {
            return new TrainVehicleOutputModel
            {
                VehicleId = entity.VehicleId,
                VehicleName = string.Empty,
                VehicleCount = entity.VehicleCount,
                Position = entity.Position,
                IsActive = entity.IsActive
            };
        }
    }
}
