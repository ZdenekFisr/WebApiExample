namespace Infrastructure.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a foreign key constraint of a rail vehicle is violated in the database.
    /// </summary>
    public class VehicleForeignKeyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleForeignKeyException"/> class. Use this constructor when the exception is thrown because of non-existent rail vehicle IDs.
        /// </summary>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="nonExistentIds">Collection of non-existent rail vehicle IDs.</param>
        public VehicleForeignKeyException(Exception innerException, ICollection<string> nonExistentIds)
            : base($"The following vehicle IDs provided in the request do not exist:{Environment.NewLine + string.Join(Environment.NewLine, nonExistentIds)}", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleForeignKeyException"/> class. Use this constructor when the exception is thrown because of a general foreign key constraint violation.
        /// </summary>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="message">Custom message.</param>
        public VehicleForeignKeyException(Exception innerException, string message)
            : base(message, innerException)
        {
        }
    }
}
