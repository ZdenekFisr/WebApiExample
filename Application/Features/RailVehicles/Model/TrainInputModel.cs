using Application.Attributes;
using Application.Common;
using Domain;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a train that consists of multiple rail vehicles. Serves as an input model to be sent to the server.
    /// </summary>
    public class TrainInputModel : ModelBase
    {
        [StringLength(Constants.VehicleNameMaxLength, MinimumLength = Constants.VehicleNameMinLength)]
        public required string Name { get; set; }

        [MaxLength(Constants.VehicleDescriptionMaxLength)]
        public string? Description { get; set; }

        /// <inheritdoc cref="Train.MaxPullForce"/>
        [Range(0, short.MaxValue, MinimumIsExclusive = true)]
        public short MaxPullForce { get; set; }

        /// <inheritdoc cref="Train.TrainVehicles"/>
        [HasUniqueValuesOfProperties("Position")]
        public required ICollection<TrainVehicleInputModel> TrainVehicles { get; set; }
    }
}
