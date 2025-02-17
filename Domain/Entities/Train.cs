using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a train that consists of multiple rail vehicles.
    /// </summary>
    public class Train : EntityWithUserBase, ICreateHistory, IUpdateHistory, ISoftDeletable
    {
        [StringLength(Constants.VehicleNameMaxLength, MinimumLength = Constants.VehicleNameMinLength)]
        public required string Name { get; set; }

        [StringLength(Constants.VehicleDescriptionMaxLength)]
        public required string Description { get; set; }

        /// <summary>
        /// The maximum pull force of the train in kN.
        /// </summary>
        public short MaxPullForce { get; set; }

        /// <summary>
        /// The collection of rail vehicle groups that are part of the train.
        /// </summary>
        public required ICollection<TrainVehicle> TrainVehicles { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
