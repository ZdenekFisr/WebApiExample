using Application.Common;

namespace Application.Features.Divisors
{
    public class WholeNumber : ModelBase
    {
        public long Value { get; init; }

        public required IEnumerable<long> Divisors { get; init; }
    }
}
