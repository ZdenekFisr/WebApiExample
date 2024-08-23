using WebApiExample.Authentication;

namespace WebApiExample.EntityInterfaces
{
    /// <summary>
    /// States that entity must have a soft-delete option and corresponding columns in the DB. Warning: add foreign key from <see cref="DeletedBy"/> to <see cref="ApplicationUser"/>.
    /// </summary>
    public interface ISoftDeletable
    {
        /// <summary>
        /// True if the row is deleted; otherwise, false.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Time as UTC when the row was last soft-deleted.
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// ID of the user who last soft-deleted the row. Warning: add foreign key to <see cref="ApplicationUser"/>.
        /// </summary>
        public string? DeletedBy { get; set; }
    }
}
