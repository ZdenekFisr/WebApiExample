using Application.Common;
using Domain.Entities;

namespace Application.Features.RailVehicles.Model
{
    /// <inheritdoc cref="TractionDiagramPoint"/>
    public class TractionDiagramPointModel : ModelBase
    {
        /// <inheritdoc cref="TractionDiagramPoint.Speed"/>
        public double Speed { get; set; }

        /// <inheritdoc cref="TractionDiagramPoint.PullForce"/>
        public double PullForce { get; set; }
    }
}
