using Application.Common;

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

        public DateTimeOffset DeletedAt { get; set; }
    }
}
