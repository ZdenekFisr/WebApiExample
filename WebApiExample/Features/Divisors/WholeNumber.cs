namespace WebApiExample.Features.Divisors
{
    public class WholeNumber : Model
    {
        public long Value { get; init; }

        public required IEnumerable<long> Divisors { get; init; }
    }
}
