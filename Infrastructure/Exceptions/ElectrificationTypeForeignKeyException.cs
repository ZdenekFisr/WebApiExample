namespace Infrastructure.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a foreign key constraint of an electrification type is violated in the database.
    /// </summary>
    public class ElectrificationTypeForeignKeyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElectrificationTypeForeignKeyException"/> class.
        /// </summary>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="message">Custom message.</param>
        public ElectrificationTypeForeignKeyException(Exception innerException, string message)
            : base(message, innerException)
        {
        }
    }
}
