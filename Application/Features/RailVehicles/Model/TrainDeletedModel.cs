using Application.Common;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a soft deleted train.
    /// </summary>
    public class TrainDeletedModel : ModelBase
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public DateTimeOffset DeletedAt { get; set; }
    }
}
