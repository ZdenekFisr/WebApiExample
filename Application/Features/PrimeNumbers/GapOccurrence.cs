using Application.Common;

namespace Application.Features.PrimeNumbers
{
    /// <summary>
    /// Represents a gap between two prime numbers and its occurrence.
    /// </summary>
    public class GapOccurrence : ModelBase
    {
        /// <summary>
        /// Difference of the two neighbouring primes.
        /// </summary>
        public long GapSize { get; set; }

        /// <summary>
        /// Number of times the specified gap size occurs.
        /// </summary>
        public int GapCount { get; set; }
    }
}
