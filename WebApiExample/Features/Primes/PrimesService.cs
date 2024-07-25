using WebApiExample.Extensions;
using WebApiExample.Helpers;

namespace WebApiExample.Features.Primes
{
    /// <inheritdoc cref="IPrimesService"/>
    public class PrimesService : IPrimesService
    {
        /// <inheritdoc />
        public IEnumerable<long> DecomposeIntoPrimes(long input)
        {
            if (input < 0)
                input = -input;

            if (input == 0 || input == 1)
            {
                yield break;
            }

            long divisor;
            while (input != 1)
            {
                divisor = 2;
                while (input % divisor != 0)
                {
                    divisor++;
                }
                input /= divisor;
                yield return divisor;
            }
        }

        /// <inheritdoc />
        public WholeNumber? FindNumberWithMostPrimes(long leftBound, long rightBound)
        {
            var result = NumberHelpers.FindNumberWithMostDecompositionParts(DecomposeIntoPrimes, leftBound, rightBound);

            if (result is null)
                return null;

            return new()
            {
                Value = result.Value.Item1,
                Primes = result.Value.Item2
            };
        }

        /// <inheritdoc />
        public PrimeGapsResult? CountGapsBetweenPrimes(long leftBound, long rightBound)
        {
            if (leftBound >= rightBound)
                return null;

            PrimeGapsResult result = new();

            for (long i = leftBound; i <= rightBound; i++)
            {
                if (i.IsPrime())
                    result.Primes.Add(i);
            }

            long gap;
            for (int i = 1; i < result.Primes.Count; i++)
            {
                gap = result.Primes[i] - result.Primes[i - 1];
                if (result.Gaps.ContainsKey(gap))
                {
                    result.Gaps[gap]++;
                }
                else
                {
                    result.Gaps.Add(gap, 1);
                }
            }

            return result;
        }
    }
}
