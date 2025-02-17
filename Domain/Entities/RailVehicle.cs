using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a rail vehicle.
    /// </summary>
    public class RailVehicle : EntityWithUserBase, ICreateHistory, IUpdateHistory, ISoftDeletable
    {
        [StringLength(Constants.VehicleNameMaxLength, MinimumLength = Constants.VehicleNameMinLength)]
        public required string Name { get; set; }

        [StringLength(Constants.VehicleDescriptionMaxLength)]
        public required string Description { get; set; }

        /// <summary>
        /// The weight of the vehicle in tonnes.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// The length of the vehicle in meters.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// The number of wheelsets.
        /// </summary>
        public byte Wheelsets { get; set; }

        /// <summary>
        /// The weight that needs to be added to the vehicle to simulate the rotating mass of the wheelsets.
        /// </summary>
        public double EquivalentRotatingWeight { get; set; }

        /// <summary>
        /// The maximum speed of the vehicle in km/h.
        /// </summary>
        public short MaxSpeed { get; set; }

        /// <summary>
        /// The constant (independent of speed) resistance of the vehicle.
        /// </summary>
        public double ResistanceConstant { get; set; }

        /// <summary>
        /// The linear (speed-dependent) resistance of the vehicle.
        /// </summary>
        public double ResistanceLinear { get; set; }

        /// <summary>
        /// The quadratic (speed-dependent squared) resistance of the vehicle, typically air resistance.
        /// </summary>
        public double ResistanceQuadratic { get; set; }

        /// <summary>
        /// The collection of the vehicle's traction systems.
        /// </summary>
        public required ICollection<VehicleTractionSystem> TractionSystems { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
