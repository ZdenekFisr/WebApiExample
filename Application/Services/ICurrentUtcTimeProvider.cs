namespace Application.Services
{
    /// <summary>
    /// Provides the current Coordinated Universal Time (UTC).
    /// </summary>
    public interface ICurrentUtcTimeProvider
    {
        /// <summary>
        /// Gets the current Coordinated Universal Time (UTC).
        /// </summary>
        /// <returns>The current date and time in UTC.</returns>
        DateTimeOffset GetCurrentUtcTime();
    }
}
