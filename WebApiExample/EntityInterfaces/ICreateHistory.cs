using WebApiExample.Authentication;

namespace WebApiExample.EntityInterfaces
{
    /// <summary>
    /// States that entity must have properties about the history of inserting rows. Warning: add foreign key from <see cref="CreatedBy"/> to <see cref="ApplicationUser"/>.
    /// </summary>
    public interface ICreateHistory
    {
        /// <summary>
        /// Time as UTC when the item (entity) was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// ID of the user who inserted the row. Warning: add foreign key to <see cref="ApplicationUser"/>.
        /// </summary>
        public string? CreatedBy { get; set; }
    }
}
