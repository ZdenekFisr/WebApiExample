using Application.Helpers;

namespace Application.Features.Divisors
{
    /// <inheritdoc cref="IDivisorsService"/>
    public class DivisorsService : IDivisorsService
    {
        /// <inheritdoc />
        public IEnumerable<long> GetDivisors(long number)
        {
            if (number == 0)
                yield break;

            if (number < 0)
                number = -number;

            for (long i = 1; i <= number / 2; i++)
            {
                if (number % i == 0)
                    yield return i;
            }
            yield return number;
        }

        /// <inheritdoc />
        public WholeNumber? FindNumberWithMostDivisors(long leftBound, long rightBound)
        {
            var result = NumberHelpers.FindNumberWithMostDecompositionParts(GetDivisors, leftBound, rightBound);

            if (result is null)
                return null;

            return new()
            {
                Value = result.Value.Item1,
                Divisors = result.Value.Item2
            };
        }
    }
}
