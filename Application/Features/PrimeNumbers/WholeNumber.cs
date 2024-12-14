using Application.Common;

namespace Application.Features.PrimeNumbers
{
    public class WholeNumber : ModelBase
    {
        public long Value { get; init; }

        public required IEnumerable<long> Primes { get; init; }
    }
}
