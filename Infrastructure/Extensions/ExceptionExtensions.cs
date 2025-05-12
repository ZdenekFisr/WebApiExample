using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    /// <summary>
    /// Extension methods for handling exceptions.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Checks if the exception is a foreign key violation.
        /// </summary>
        /// <param name="exception">The exception instance.</param>
        /// <returns>True if the exception is a foreign key violation; otherwise, false.</returns>
        public static bool IsForeignKeyViolation(this DbUpdateException exception)
        {
            return exception.InnerException is SqlException sqlException && sqlException.Number == 547;
        }
    }
}
