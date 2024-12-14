using System.ComponentModel.DataAnnotations;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a rail vehicle that can run on non-electrified tracks and also use the electrification.
    /// </summary>
    public class RailVehicleHybridModel : RailVehicleDrivingModelBase
    {
        /// <summary>
        /// Efficiency of the vehicle when running on electrified tracks in the range (0, 1>.
        /// </summary>
        [Range(0, 1, MinimumIsExclusive = true)]
        public double EfficiencyDependent { get; set; }

        /// <summary>
        /// Efficiency of the vehicle when running on non-electrified tracks in the range (0, 1>.
        /// </summary>
        [Range(0, 1, MinimumIsExclusive = true)]
        public double EfficiencyIndependent { get; set; }

        /// <summary>
        /// Maximum speed in km/h when running on non-electrified tracks (usually lower than on electrified tracks).
        /// </summary>
        [Range(0, short.MaxValue, MinimumIsExclusive = true)]
        public short MaxSpeedHybrid { get; set; }

        /// <summary>
        /// Performance (power output) in kW when running on non-electrified tracks (usually lower than on electrified tracks).
        /// </summary>
        [Range(0, short.MaxValue, MinimumIsExclusive = true)]
        public short PerformanceHybrid { get; set; }
    }
}
