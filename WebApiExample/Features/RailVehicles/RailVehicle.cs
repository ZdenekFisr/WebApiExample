using System.ComponentModel.DataAnnotations;
using WebApiExample.EntityInterfaces;

namespace WebApiExample.Features.RailVehicles
{
    public class RailVehicle : EntityWithUser, ISoftDeletable
    {
        [StringLength(Constants.VehicleNameMaxLength)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        public double Weight { get; set; }

        public double Performance { get; set; }

        public short MaxSpeed { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }
    }
}
