using Application.Common;
using Domain.Entities;

namespace Application.Features.RailVehicles.Model
{
    /// <inheritdoc cref="ElectrificationType"/>
    public class ElectrificationTypeListModel : ModelBase
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        /// <inheritdoc cref="ElectrificationType.Voltage"/>
        public int Voltage { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
