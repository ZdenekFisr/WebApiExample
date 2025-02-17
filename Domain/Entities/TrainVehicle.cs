using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a group of rail vehicles that are part of a train. If they use their tractive effort, they are active.
    /// </summary>
    [Table("TrainVehicles")]
    public class TrainVehicle : EntityBase
    {
        public Guid VehicleId { get; set; }

        /// <summary>
        /// The count of the vehicles of the same type.
        /// </summary>
        public short VehicleCount { get; set; }

        /// <summary>
        /// The position of the vehicle group in the train.
        /// </summary>
        public short Position { get; set; }

        /// <summary>
        /// Indicates whether the vehicles use their tractive effort.
        /// </summary>
        public bool IsActive { get; set; }

        public Guid TrainId { get; set; }
    }
}
