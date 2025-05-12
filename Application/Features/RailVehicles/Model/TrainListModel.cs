using Application.Common;
using Application.Features.RailVehicles.Extensions;
using Domain.Entities;

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

        /// <summary>
        /// Creates a new instance of <see cref="TrainListModel"/> from a <see cref="Train"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="Train"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="TrainListModel"/>.</returns>
        public static TrainListModel FromEntity(Train entity)
        {
            var model = new TrainListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                TrainVehicles = [.. entity.TrainVehicles.Select(TrainVehicleOutputModel.FromEntity)]
            };
            model.Arrangement = model.GetArrangement();

            return model;
        }
    }
}
