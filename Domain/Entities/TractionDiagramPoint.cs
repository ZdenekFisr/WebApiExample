using Domain.Common;

namespace Domain.Entities
{
    public class TractionDiagramPoint : EntityBase
    {
        public double Speed { get; set; }

        public double PullForce { get; set; }

        public Guid RailVehicleId { get; set; }
    }
}
