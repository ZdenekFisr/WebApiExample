using System.ComponentModel.DataAnnotations;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a rail vehicle that depends on electrification to run (e.g. overhead lines or third rail).
    /// </summary>
    public class RailVehicleDependentModel : RailVehicleDrivingModelBase
    {
        /// <summary>
        /// Efficiency of the vehicle in the range (0, 1>.
        /// </summary>
        [Range(0, 1, MinimumIsExclusive = true)]
        public double Efficiency { get; set; }
    }
}
