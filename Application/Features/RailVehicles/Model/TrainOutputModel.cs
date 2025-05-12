using Application.Common;
using Domain.Entities;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a train that consists of multiple rail vehicles. Serves as an output model to be retrieved from the server.
    /// </summary>
    public class TrainOutputModel : ModelBase
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        /// <inheritdoc cref="Train.MaxPullForce"/>
        public short MaxPullForce { get; set; }

        /// <inheritdoc cref="Train.TrainVehicles"/>
        public required ICollection<TrainVehicleOutputModel> TrainVehicles { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="TrainOutputModel"/> from a <see cref="Train"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="Train"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="TrainOutputModel"/>.</returns>
        public static TrainOutputModel FromEntity(Train entity)
        {
            return new TrainOutputModel
            {
                Name = entity.Name,
                Description = entity.Description,
                MaxPullForce = entity.MaxPullForce,
                TrainVehicles = [.. entity.TrainVehicles.Select(TrainVehicleOutputModel.FromEntity)]
            };
        }
    }
}
