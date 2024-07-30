namespace WebApiExample.Helpers
{
    /// <summary>
    /// Contains helper methods that deal with <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeHelpers
    {
        /// <inheritdoc cref="DateTime.UtcNow"/>
        public static DateTime GetCurrentDateTimeUtc()
            => DateTime.UtcNow;
    }
}
