using Application.Common;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a group of rail vehicles that are part of a train. If they use their tractive effort, they are active. Serves as an input model to be sent to the server.
    /// </summary>
    public class TrainVehicleInputModel : ModelBase
    {
        public Guid VehicleId { get; set; }

        /// <inheritdoc cref="TrainVehicle.VehicleCount"/>
        [Range(0, short.MaxValue, MinimumIsExclusive = true)]
        public short VehicleCount { get; set; }

        /// <inheritdoc cref="TrainVehicle.Position"/>
        public short Position { get; set; }

        /// <inheritdoc cref="TrainVehicle.IsActive"/>
        public bool IsActive { get; set; }
    }
}
