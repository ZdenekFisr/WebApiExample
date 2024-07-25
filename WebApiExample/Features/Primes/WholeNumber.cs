namespace WebApiExample.Features.Primes
{
    public class WholeNumber : Model
    {
        public long Value { get; init; }

        public required IEnumerable<long> Primes { get; init; }
    }
}
