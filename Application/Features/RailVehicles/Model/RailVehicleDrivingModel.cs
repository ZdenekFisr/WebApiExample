using Application.Features.RailVehicles.Attributes;
using Domain.Entities;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a driving rail vehicle.
    /// </summary>
    [ValidTractionSystem]
    public class RailVehicleDrivingModel : RailVehicleModelBase
    {
        /// <inheritdoc cref="RailVehicle.TractionSystems"/>
        public required ICollection<VehicleTractionSystemModel> TractionSystems { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="RailVehicleDrivingModel"/> from a <see cref="RailVehicle"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="RailVehicle"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="RailVehicleDrivingModel"/>.</returns>
        public static RailVehicleDrivingModel FromEntity(RailVehicle entity)
        {
            return new RailVehicleDrivingModel
            {
                Name = entity.Name,
                Description = entity.Description,
                Weight = entity.Weight,
                Length = entity.Length,
                Wheelsets = entity.Wheelsets,
                EquivalentRotatingWeight = entity.EquivalentRotatingWeight,
                MaxSpeed = entity.MaxSpeed,
                ResistanceConstant = entity.ResistanceConstant,
                ResistanceLinear = entity.ResistanceLinear,
                ResistanceQuadratic = entity.ResistanceQuadratic,
                TractionSystems = [.. entity.TractionSystems.Select(VehicleTractionSystemModel.FromEntity)]
            };
        }

        /// <summary>
        /// Converts the current <see cref="RailVehicleDrivingModel"/> instance to a <see cref="RailVehicle"/> entity.
        /// </summary>
        /// <returns>A new instance of <see cref="RailVehicle"/>.</returns>
        public override RailVehicle ToEntity()
        {
            return new RailVehicle
            {
                UserId = string.Empty,
                Name = Name,
                Description = Description,
                Weight = Weight,
                Length = Length,
                Wheelsets = Wheelsets,
                EquivalentRotatingWeight = EquivalentRotatingWeight,
                MaxSpeed = MaxSpeed,
                ResistanceConstant = ResistanceConstant,
                ResistanceLinear = ResistanceLinear,
                ResistanceQuadratic = ResistanceQuadratic,
                TractionSystems = [.. TractionSystems.Select(x => x.ToEntity())]
            };
        }
    }
}
