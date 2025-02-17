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
    }
}
