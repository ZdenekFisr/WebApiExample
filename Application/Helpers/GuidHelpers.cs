namespace Application.Helpers
{
    /// <summary>
    /// Provides helper methods for working with GUIDs.
    /// </summary>
    public static class GuidHelpers
    {
        /// <summary>
        /// Generates a specified number of random GUIDs.
        /// </summary>
        /// <param name="count">The number of GUIDs to generate.</param>
        /// <returns>An enumerable collection of random GUIDs.</returns>
        public static IEnumerable<Guid> GenerateRandomGuids(int count)
            => Enumerable.Range(0, count).Select(_ => Guid.NewGuid());
    }
}
