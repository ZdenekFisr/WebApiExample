using Application.Common;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a train that consists of multiple rail vehicles.
    /// </summary>
    public class TrainListModel : ModelBase
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        /// <inheritdoc cref="Train.TrainVehicles"/>
        public ICollection<TrainVehicleOutputModel>? TrainVehicles { get; set; }

        /// <summary>
        /// The arrangement of the vehicles.
        /// </summary>
        public string? Arrangement { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
