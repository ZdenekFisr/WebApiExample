namespace Domain.Common
{
    /// <summary>
    /// States that entity must have properties about the history of updating rows. Warning: add foreign key from <see cref="UpdatedBy"/> to the user table.
    /// </summary>
    public interface IUpdateHistory
    {
        /// <summary>
        /// Time as UTC when the item (entity) was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// ID of the user who last updated the row. Warning: add foreign key to the user table.
        /// </summary>
        public string? UpdatedBy { get; set; }
    }
}
