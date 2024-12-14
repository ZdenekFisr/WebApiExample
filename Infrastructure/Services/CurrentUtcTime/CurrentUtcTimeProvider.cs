namespace Infrastructure.Services.CurrentUtcTime
{
    /// <inheritdoc />
    public class CurrentUtcTimeProvider : ICurrentUtcTimeProvider
    {
        /// <inheritdoc />
        public DateTimeOffset GetCurrentUtcTime()
            => DateTimeOffset.UtcNow;
    }
}
