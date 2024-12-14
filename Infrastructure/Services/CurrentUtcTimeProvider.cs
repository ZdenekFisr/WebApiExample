using Application.Services;

namespace Infrastructure.Services
{
    /// <inheritdoc />
    public class CurrentUtcTimeProvider : ICurrentUtcTimeProvider
    {
        /// <inheritdoc />
        public DateTimeOffset GetCurrentUtcTime()
            => DateTimeOffset.UtcNow;
    }
}
