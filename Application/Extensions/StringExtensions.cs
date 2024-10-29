namespace Application.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string into a collection of integers. If a part of the string can't be converted, that part is skipped.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="separator">A character used to split the string by.</param>
        /// <param name="allowZero">Include zero values into the result.</param>
        /// <param name="allowNegative">Include negative values into the result.</param>
        /// <returns>IEnumerable collection of integers.</returns>
        public static IEnumerable<int> ToIntCollection(this string input, char separator, bool allowZero = true, bool allowNegative = true)
        {
            string[] stringArray = input.Split(separator);
            bool parseSuccess;
            foreach (string str in stringArray)
            {
                parseSuccess = int.TryParse(str, out int value);

                if (!allowZero && value == 0 || !allowNegative && value < 0)
                    continue;

                if (parseSuccess)
                    yield return value;
            }
        }
    }
}
