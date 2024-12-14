using System.ComponentModel.DataAnnotations;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a rail vehicle that doesn't need electrification to run (e.g. diesel, steam or battery-powered trains).
    /// </summary>
    public class RailVehicleIndependentModel : RailVehicleDrivingModelBase
    {
        /// <summary>
        /// Efficiency of the vehicle in the range (0, 1>.
        /// </summary>
        [Range(0, 1, MinimumIsExclusive = true)]
        public double Efficiency { get; set; }
    }
}
