using System.ComponentModel.DataAnnotations;

namespace WebApiExample.Features.RailVehicles
{
    public class RailVehicleModel : Model
    {
        [StringLength(Constants.VehicleNameMaxLength)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        public double Weight { get; set; }

        public double Performance { get; set; }

        public short MaxSpeed { get; set; }
    }
}
