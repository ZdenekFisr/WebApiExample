using Application.Common;
using Domain.Entities;

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

        public DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="TrainDeletedModel"/> from a <see cref="Train"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="Train"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="TrainDeletedModel"/>.</returns>
        public static TrainDeletedModel FromEntity(Train entity)
        {
            return new TrainDeletedModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DeletedAt = entity.DeletedAt
            };
        }
    }
}
