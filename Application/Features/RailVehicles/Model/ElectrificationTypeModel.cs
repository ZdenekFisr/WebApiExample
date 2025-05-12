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

        /// <summary>
        /// Creates a new instance of <see cref="ElectrificationTypeModel"/> from a <see cref="ElectrificationType"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="ElectrificationType"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="ElectrificationTypeModel"/>.</returns>
        public static ElectrificationTypeModel FromEntity(ElectrificationType entity)
        {
            return new ElectrificationTypeModel
            {
                Name = entity.Name,
                Description = entity.Description,
                Voltage = entity.Voltage
            };
        }

        /// <summary>
        /// Converts the current <see cref="ElectrificationTypeModel"/> instance to a <see cref="ElectrificationType"/> entity.
        /// </summary>
        /// <returns>A new instance of <see cref="ElectrificationType"/>.</returns>
        public ElectrificationType ToEntity()
        {
            return new ElectrificationType
            {
                UserId = string.Empty,
                Name = Name,
                Description = Description,
                Voltage = Voltage
            };
        }
    }
}
