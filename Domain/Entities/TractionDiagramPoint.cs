using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a point in the traction diagram with speed and pull force values.
    /// </summary>
    [Table("TractionDiagramPoints")]
    public class TractionDiagramPoint : EntityBase
    {
        /// <summary>
        /// The speed of the vehicle in km/h (X-axis of the diagram).
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// The pull force of the vehicle in kN (Y-axis of the diagram).
        /// </summary>
        public double PullForce { get; set; }

        public Guid VehicleTractionSystemId { get; set; }
    }
}
