using Application.Common;
using Domain.Entities;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a driving rail vehicle.
    /// </summary>
    public class RailVehicleDrivingListModel : ModelBase
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        /// <inheritdoc cref="RailVehicle.MaxSpeed"/>
        public short MaxSpeed { get; set; }

        /// <summary>
        /// The maximum performance of the vehicle in kW.
        /// </summary>
        public short Performance { get; set; }

        /// <summary>
        /// The maximum pull force of the vehicle in kN.
        /// </summary>
        public short MaxPullForce { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="RailVehicleDrivingListModel"/> from a <see cref="RailVehicle"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="RailVehicle"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="RailVehicleDrivingListModel"/>.</returns>
        public static RailVehicleDrivingListModel FromEntity(RailVehicle entity)
        {
            return new RailVehicleDrivingListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                MaxSpeed = entity.MaxSpeed,
                Performance = entity.TractionSystems.Max(vts => vts.Performance),
                MaxPullForce = entity.TractionSystems.Max(vts => vts.MaxPullForce),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
