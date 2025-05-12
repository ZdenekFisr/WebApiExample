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

        /// <summary>
        /// Creates a new instance of <see cref="TractionDiagramPointModel"/> from a <see cref="TractionDiagramPoint"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="TractionDiagramPoint"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="TractionDiagramPointModel"/>.</returns>
        public static TractionDiagramPointModel FromEntity(TractionDiagramPoint entity)
        {
            return new TractionDiagramPointModel
            {
                Speed = entity.Speed,
                PullForce = entity.PullForce
            };
        }

        /// <summary>
        /// Converts the current <see cref="TractionDiagramPointModel"/> instance to a <see cref="TractionDiagramPoint"/> entity.
        /// </summary>
        /// <returns>A new instance of <see cref="TractionDiagramPoint"/>.</returns>
        public TractionDiagramPoint ToEntity()
        {
            return new TractionDiagramPoint
            {
                Speed = Speed,
                PullForce = PullForce
            };
        }
    }
}
