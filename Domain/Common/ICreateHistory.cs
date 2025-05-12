namespace Domain.Common
{
    /// <summary>
    /// States that entity must have properties about the history of inserting rows. Warning: add foreign key from <see cref="CreatedBy"/> to the user table.
    /// </summary>
    public interface ICreateHistory
    {
        /// <summary>
        /// Time as UTC when the item (entity) was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// ID of the user who inserted the row. Warning: add foreign key to the user table.
        /// </summary>
        string? CreatedBy { get; set; }
    }
}
