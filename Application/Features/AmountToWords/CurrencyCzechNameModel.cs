using Application.Common;
using Domain.Enums;

namespace Application.Features.AmountToWords
{
    public class CurrencyCzechNameModel : ModelBase
    {
        public required string Code { get; set; }

        public required string OneUnit { get; set; }

        public required string TwoToFourUnits { get; set; }

        public required string FiveOrMoreUnits { get; set; }

        public required GrammaticalGender UnitGrammaticalGender { get; set; }

        public required string OneSubunit { get; set; }

        public required string TwoToFourSubunits { get; set; }

        public required string FiveOrMoreSubunits { get; set; }

        public required GrammaticalGender SubunitGrammaticalGender { get; set; }
    }
}
