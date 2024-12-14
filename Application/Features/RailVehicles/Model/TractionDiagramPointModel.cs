using Application.Common;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a point on a traction (speed-force) diagram.
    /// </summary>
    public class TractionDiagramPointModel : ModelBase
    {
        /// <summary>
        /// Speed in km/h.
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Pull force in kN.
        /// </summary>
        public double PullForce { get; set; }
    }
}
