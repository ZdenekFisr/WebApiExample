using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Represents the traction system of a vehicle and its capabilities.
    /// </summary>
    [Table("VehicleTractionSystems")]
    public class VehicleTractionSystem : EntityBase
    {
        public Guid VehicleId { get; set; }

        /// <summary>
        /// If null, the vehicle can run without electrification.
        /// </summary>
        public Guid? ElectrificationTypeId { get; set; }

        /// <summary>
        /// The coefficient of the voltage that represents the real voltage under the electrification type. If null, the vehicle can run without electrification.
        /// </summary>
        public double? VoltageCoefficient { get; set; }

        /// <summary>
        /// The number of wheelsets that are driven by the traction system.
        /// </summary>
        public byte DrivingWheelsets { get; set; }

        /// <summary>
        /// The maximum speed of the traction system in km/h.
        /// </summary>
        public short MaxSpeed { get; set; }

        /// <summary>
        /// The performance of the traction system in kW.
        /// </summary>
        public short Performance { get; set; }

        /// <summary>
        /// The maximum pull force of the traction system in kN.
        /// </summary>
        public short MaxPullForce { get; set; }

        /// <summary>
        /// The efficiency of the traction system in the range (0;1>.
        /// </summary>
        public double Efficiency { get; set; }

        /// <summary>
        /// The collection of points that represent the traction diagram of the traction system.
        /// </summary>
        public required ICollection<TractionDiagramPoint> TractionDiagram { get; set; }
    }
}
