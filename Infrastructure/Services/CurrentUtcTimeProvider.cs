using Application.Services;

namespace Infrastructure.Services
{
    /// <inheritdoc cref="ICurrentUtcTimeProvider"/>
    public class CurrentUtcTimeProvider : ICurrentUtcTimeProvider
    {
        /// <inheritdoc />
        public DateTimeOffset GetCurrentUtcTime()
            => DateTimeOffset.UtcNow;
    }
}
