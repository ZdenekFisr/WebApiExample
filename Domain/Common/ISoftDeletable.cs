namespace Domain.Common
{
    /// <summary>
    /// States that entity must have a soft-delete option and corresponding columns in the DB. Warning: add foreign key from <see cref="DeletedBy"/> to the user table.
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
        public DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        /// ID of the user who last soft-deleted the row. Warning: add foreign key to the user table.
        /// </summary>
        public string? DeletedBy { get; set; }
    }
}
