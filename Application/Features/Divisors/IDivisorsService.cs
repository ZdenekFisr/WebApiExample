namespace Application.Features.Divisors
{
    /// <summary>
    /// Contains methods for finding divisors of whole numbers.
    /// </summary>
    public interface IDivisorsService
    {
        /// <summary>
        /// Finds divisors of a whole number.
        /// </summary>
        /// <param name="number">Input number.</param>
        /// <returns>IEnumerable collection of divisors.</returns>
        public IEnumerable<long> GetDivisors(long number);

        /// <summary>
        /// Finds the number with the most divisors within a specified range.
        /// </summary>
        /// <param name="leftBound">The lowest number of the input range.</param>
        /// <param name="rightBound">The highest number of the input range.</param>
        /// <returns>Number with the most divisors and an IEnumerable collection of its divisors.</returns>
        public WholeNumber? FindNumberWithMostDivisors(long leftBound, long rightBound);
    }
}
