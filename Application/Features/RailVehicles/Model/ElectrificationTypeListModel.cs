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

        /// <summary>
        /// Creates a new instance of <see cref="ElectrificationTypeListModel"/> from a <see cref="ElectrificationType"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="ElectrificationType"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="ElectrificationTypeListModel"/>.</returns>
        public static ElectrificationTypeListModel FromEntity(ElectrificationType entity)
        {
            return new ElectrificationTypeListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Voltage = entity.Voltage,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
