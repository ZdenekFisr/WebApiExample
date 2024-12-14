using Application.Common;

namespace Application.Features.RailVehicles.ListModel
{
    /// <summary>
    /// Model for listing driving rail vehicles.
    /// </summary>
    public class RailVehicleListModel : ModelBase
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double MaxSpeed { get; set; }
        public double Performance { get; set; }
        public double MaxPullForce { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
