namespace WebApiExample.Helpers
{
    /// <summary>
    /// Contains helper methods that deal with numbers.
    /// </summary>
    public static class NumberHelpers
    {
        /// <summary>
        /// Calls a method that decomposes whole numbers within a specified range into whole number parts and finds the number with the most parts.
        /// </summary>
        /// <param name="decompositionMethod">Function that decomposes a whole number into whole number parts.</param>
        /// <param name="leftBound">The lowest number of the input range.</param>
        /// <param name="rightBound">The highest number of the input range.</param>
        /// <returns>Number with the most parts and an IEnumerable collection of the parts.</returns>
        public static (long, IEnumerable<long>)? FindNumberWithMostDecompositionParts(Func<long, IEnumerable<long>> decompositionMethod, long leftBound, long rightBound)
        {
            if (leftBound >= rightBound || leftBound <= 0)
                return null;

            long resultNumber = leftBound;
            int partsCount = 0;
            IEnumerable<long> parts, resultParts = [];

            for (long i = leftBound; i <= rightBound; i++)
            {
                parts = decompositionMethod(i);
                if (parts.Count() > partsCount)
                {
                    resultNumber = i;
                    resultParts = parts;
                    partsCount = parts.Count();
                }
            }

            return (resultNumber, resultParts);
        }
    }
}
