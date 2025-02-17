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
    }
}
