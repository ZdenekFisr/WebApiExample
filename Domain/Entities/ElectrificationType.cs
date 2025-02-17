using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Represents the type of electrification.
    /// </summary>
    [Table("ElectrificationTypes")]
    public class ElectrificationType : EntityWithUserBase, ICreateHistory, IUpdateHistory, ISoftDeletable
    {
        [StringLength(Constants.VehicleNameMaxLength, MinimumLength = Constants.VehicleNameMinLength)]
        public required string Name { get; set; }

        [StringLength(Constants.VehicleDescriptionMaxLength)]
        public required string Description { get; set; }

        /// <summary>
        /// The voltage of the electrification type in volts.
        /// </summary>
        public int Voltage { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
