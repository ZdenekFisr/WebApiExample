using Application.Features.RailVehicles.Attributes;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.RailVehicles.Model
{
    /// <inheritdoc cref="VehicleTractionSystem"/>
    [ValidTractionDiagram]
    public class VehicleTractionSystemModel
    {
        public Guid? ElectrificationTypeId { get; set; }

        /// <inheritdoc cref="VehicleTractionSystem.VoltageCoefficient"/>
        [Range(0, double.MaxValue, MinimumIsExclusive = true)]
        public double? VoltageCoefficient { get; set; }

        /// <inheritdoc cref="VehicleTractionSystem.DrivingWheelsets"/>
        [Range(0, byte.MaxValue, MinimumIsExclusive = true)]
        public byte DrivingWheelsets { get; set; }

        /// <inheritdoc cref="VehicleTractionSystem.MaxSpeed"/>
        [Range(0, short.MaxValue, MinimumIsExclusive = true)]
        public short MaxSpeed { get; set; }

        /// <inheritdoc cref="VehicleTractionSystem.Performance"/>
        [Range(0, short.MaxValue, MinimumIsExclusive = true)]
        public short Performance { get; set; }

        /// <inheritdoc cref="VehicleTractionSystem.MaxPullForce"/>
        [Range(0, short.MaxValue, MinimumIsExclusive = true)]
        public short MaxPullForce { get; set; }

        /// <inheritdoc cref="VehicleTractionSystem.Efficiency"/>
        [Range(0, 1, MinimumIsExclusive = true)]
        public double Efficiency { get; set; }

        /// <inheritdoc cref="VehicleTractionSystem.TractionDiagram"/>
        public required ICollection<TractionDiagramPointModel> TractionDiagram { get; set; }
    }
}
