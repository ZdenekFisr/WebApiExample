using Application.Common;
using Domain;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a base class for all rail vehicles.
    /// </summary>
    public abstract class RailVehicleModelBase : ModelBase
    {
        [StringLength(Constants.VehicleNameMaxLength, MinimumLength = Constants.VehicleNameMinLength)]
        public required string Name { get; set; }

        [StringLength(Constants.VehicleDescriptionMaxLength)]
        public required string Description { get; set; }

        /// <summary>
        /// Weight in metric tons without cargo or personnel.
        /// </summary>
        [Range(0, double.MaxValue, MinimumIsExclusive = true)]
        public double Weight { get; set; }

        /// <summary>
        /// Length over buffers in meters.
        /// </summary>
        [Range(0, double.MaxValue, MinimumIsExclusive = true)]
        public double Length { get; set; }

        /// <summary>
        /// Number of wheelsets.
        /// </summary>
        [Range(0, byte.MaxValue, MinimumIsExclusive = true)]
        public byte Wheelsets { get; set; }

        /// <summary>
        /// A fictional weight that needs to be added to dynamic calculations to account for rotating parts, such as wheelsets or rotors.
        /// </summary>
        [Range(0, double.MaxValue)]
        public double EquivalentRotatingWeight { get; set; }

        /// <summary>
        /// Maximum speed in km/h.
        /// </summary>
        [Range(0, short.MaxValue, MinimumIsExclusive = true)]
        public short MaxSpeed { get; set; }

        /// <summary>
        /// The resistance constant that is independent of the speed. Typically in 1÷3 range.
        /// </summary>
        [Range(0, double.MaxValue)]
        public double ResistanceConstant { get; set; }

        /// <summary>
        /// The resistance constant that is linearly dependent on the speed. Typically in 0÷0.002 range. In some cases it's negligible.
        /// </summary>
        [Range(0, double.MaxValue)]
        public double ResistanceLinear { get; set; }

        /// <summary>
        /// The resistance constant that is quadratically dependent on the speed. Typically in 0.0001÷0.001 range. The most dominant factor is the air resistance.
        /// </summary>
        [Range(0, double.MaxValue)]
        public double ResistanceQuadratic { get; set; }
    }
}
