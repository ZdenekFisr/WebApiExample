using Domain.Entities;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// Represents a pulled rail vehicle.
    /// </summary>
    public class RailVehiclePulledModel : RailVehicleModelBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="RailVehiclePulledModel"/> from a <see cref="RailVehicle"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="RailVehicle"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="RailVehiclePulledModel"/>.</returns>
        public static RailVehiclePulledModel FromEntity(RailVehicle entity)
        {
            return new RailVehiclePulledModel
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
                ResistanceQuadratic = entity.ResistanceQuadratic
            };
        }

        /// <summary>
        /// Converts the current <see cref="RailVehiclePulledModel"/> instance to a <see cref="RailVehicle"/> entity.
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
                TractionSystems = []
            };
        }
    }
}
