namespace Application.Features.PrimeNumbers
{
    /// <summary>
    /// Contains methods that deal with prime numbers.
    /// </summary>
    public interface IPrimeNumbersService
    {
        /// <summary>
        /// Decomposes a whole number into primes whose product is equal to the input number.
        /// </summary>
        /// <param name="input">Whole number.</param>
        /// <returns>IEnumerable collection of primes.</returns>
        IEnumerable<long> DecomposeIntoPrimes(long input);

        /// <summary>
        /// Finds the number that can be decomposed into the largest number of primes within a specified range.
        /// </summary>
        /// <param name="leftBound">The lowest number of the input range.</param>
        /// <param name="rightBound">The highest number of the input range.</param>
        /// <returns>Number that can be decomposed into the largest number of primes and an IEnumerable collection of those primes.</returns>
        WholeNumber? FindNumberWithMostPrimes(long leftBound, long rightBound);

        /// <summary>
        /// Counts all gaps between each pair of neighbouring primes that are present within a specified range by size.
        /// </summary>
        /// <param name="leftBound">The lowest number of the input range.</param>
        /// <param name="rightBound">The highest number of the input range.</param>
        /// <returns>List of primes and a list of gap counts by size.</returns>
        PrimeGapsResult? CountGapsBetweenPrimes(long leftBound, long rightBound);
    }
}
