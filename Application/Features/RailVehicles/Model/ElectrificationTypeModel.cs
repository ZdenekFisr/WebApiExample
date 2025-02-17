using Application.Common;
using Domain;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.RailVehicles.Model
{
    /// <inheritdoc cref="ElectrificationType"/>
    public class ElectrificationTypeModel : ModelBase
    {
        [StringLength(Constants.VehicleNameMaxLength, MinimumLength = Constants.VehicleNameMinLength)]
        public required string Name { get; set; }

        [StringLength(Constants.VehicleDescriptionMaxLength)]
        public required string Description { get; set; }

        /// <inheritdoc cref="ElectrificationType.Voltage"/>
        [Range(0, int.MaxValue, MinimumIsExclusive = true)]
        public int Voltage { get; set; }
    }
}
