namespace Infrastructure.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a request is unauthorized.
    /// </summary>
    public class UnauthorizedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
        /// </summary>
        public UnauthorizedException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">Error message.</param>
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
