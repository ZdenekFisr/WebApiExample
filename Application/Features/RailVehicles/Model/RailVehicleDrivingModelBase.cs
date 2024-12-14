using Application.Attributes;
using Application.Features.RailVehicles.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a base class for all rail vehicle that have a drive (e.g. locomotives, EMUs, DMUs, ...).
    /// </summary>
    [ValidTractionDiagram]
    public abstract class RailVehicleDrivingModelBase : RailVehicleModelBase
    {
        /// <summary>
        /// Number of wheelsets that are driven by the vehicle's motors.
        /// </summary>
        [Range(0, byte.MaxValue, MinimumIsExclusive = true)]
        [LowerOrEqual(nameof(Wheelsets))]
        public byte DrivingWheelsets { get; set; }

        /// <summary>
        /// Performance (power output) in kW.
        /// </summary>
        [Range(0, short.MaxValue, MinimumIsExclusive = true)]
        public short Performance { get; set; }

        /// <summary>
        /// Maximum pull force (tractive effort) in kN.
        /// </summary>
        [Range(0, short.MaxValue, MinimumIsExclusive = true)]
        public short MaxPullForce { get; set; }

        /// <summary>
        /// Collection of points representing the traction (speed-force) diagram as one polyline.
        /// </summary>
        public required ICollection<TractionDiagramPointModel> TractionDiagram { get; set; }
    }
}
