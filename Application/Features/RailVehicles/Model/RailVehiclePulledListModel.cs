using Application.Common;
using Domain.Entities;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a pulled rail vehicle.
    /// </summary>
    public class RailVehiclePulledListModel : ModelBase
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        /// <inheritdoc cref="RailVehicle.MaxSpeed"/>
        public short MaxSpeed { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="RailVehiclePulledListModel"/> from a <see cref="RailVehicle"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="RailVehicle"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="RailVehiclePulledListModel"/>.</returns>
        public static RailVehiclePulledListModel FromEntity(RailVehicle entity)
        {
            return new RailVehiclePulledListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                MaxSpeed = entity.MaxSpeed,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
