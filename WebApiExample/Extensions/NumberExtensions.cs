namespace WebApiExample.Extensions
{
    public static class NumberExtensions
    {
        /// <summary>
        /// Indicates whether a decimal number is also a whole number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>True if the input is a whole number; otherwise, false.</returns>
        public static bool IsWholeNumber(this decimal number)
            => number == Math.Truncate(number);

        /// <summary>
        /// Indicates whether a whole number is a prime number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>True if the input is a prime number; otherwise, false.</returns>
        public static bool IsPrime(this long number)
        {
            if (number < 2)
                return false;

            for (int i = 2; i <= number / 2; i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }
    }
}
