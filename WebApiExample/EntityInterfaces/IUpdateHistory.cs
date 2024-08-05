namespace WebApiExample.EntityInterfaces
{
    /// <summary>
    /// States that entity must have properties about the history of updating rows. Warning: add foreign key from <see cref="UpdatedBy"/> to <see cref="ApplicationUser"/>.
    /// </summary>
    public interface IUpdateHistory
    {
        /// <summary>
        /// Time as UTC when the item (entity) was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// ID of the user who last updated the row. Warning: add foreign key to <see cref="ApplicationUser"/>.
        /// </summary>
        public string? UpdatedBy { get; set; }
    }
}
