namespace WebApiExample.Features.Primes
{
    /// <summary>
    /// Result of finding gaps between primes.
    /// </summary>
    public class PrimeGapsResult : Model
    {
        /// <summary>
        /// List of primes.
        /// </summary>
        public List<long> Primes { get; set; } = [];

        /// <summary>
        /// Gap counts by size.
        /// </summary>
        public Dictionary<long, int> Gaps { get; set; } = [];
    }
}
