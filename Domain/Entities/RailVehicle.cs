using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class RailVehicle : EntityWithUserBase, ICreateHistory, IUpdateHistory, ISoftDeletable
    {
        [StringLength(Constants.VehicleNameMaxLength)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        public double Weight { get; set; }

        public double Length { get; set; }

        public byte Wheelsets { get; set; }

        public double EquivalentRotatingWeight { get; set; }

        public short MaxSpeed { get; set; }

        public double ResistanceConstant { get; set; }

        public double ResistanceLinear { get; set; }

        public double ResistanceQuadratic { get; set; }

        public byte DrivingWheelsets { get; set; }

        public short? MaxSpeedHybrid { get; set; }

        public short Performance { get; set; }

        public short? PerformanceHybrid { get; set; }

        public short MaxPullForce { get; set; }

        public double? EfficiencyDependent { get; set; }

        public double? EfficiencyIndependent { get; set; }

        public required ICollection<TractionDiagramPoint> TractionDiagram { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
