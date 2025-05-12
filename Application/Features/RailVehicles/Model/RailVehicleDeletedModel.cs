using Application.Common;
using Domain.Entities;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a soft deleted rail vehicle.
    /// </summary>
    public class RailVehicleDeletedModel : ModelBase
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="RailVehicleDeletedModel"/> from a <see cref="RailVehicle"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="RailVehicle"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="RailVehicleDeletedModel"/>.</returns>
        public static RailVehicleDeletedModel FromEntity(RailVehicle entity)
        {
            return new RailVehicleDeletedModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DeletedAt = entity.DeletedAt
            };
        }
    }
}
