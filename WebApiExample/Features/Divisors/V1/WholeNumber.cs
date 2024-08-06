namespace WebApiExample.Features.Divisors.V1
{
    public class WholeNumber : Model
    {
        public long Value { get; init; }

        public required IEnumerable<long> Divisors { get; init; }
    }
}
