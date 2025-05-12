using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Represents the Czech names of a currency, including its units and subunits.
    /// </summary>
    public class CurrencyCzechName : EntityBase
    {
        /// <summary>
        /// The international code of the currency.
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// The name of the currency in singular form.
        /// </summary>
        public required string OneUnit { get; set; }

        /// <summary>
        /// The name of the currency in plural form for 2 to 4 units.
        /// </summary>
        public required string TwoToFourUnits { get; set; }

        /// <summary>
        /// The name of the currency in plural form for 5 or more units.
        /// </summary>
        public required string FiveOrMoreUnits { get; set; }

        /// <summary>
        /// The grammatical gender of the currency unit.
        /// </summary>
        public required GrammaticalGender UnitGrammaticalGender { get; set; }

        /// <summary>
        /// The name of the currency subunit in singular form.
        /// </summary>
        public required string OneSubunit { get; set; }

        /// <summary>
        /// The name of the currency subunit in plural form for 2 to 4 subunits.
        /// </summary>
        public required string TwoToFourSubunits { get; set; }

        /// <summary>
        /// The name of the currency subunit in plural form for 5 or more subunits.
        /// </summary>
        public required string FiveOrMoreSubunits { get; set; }

        /// <summary>
        /// The grammatical gender of the currency subunit.
        /// </summary>
        public required GrammaticalGender SubunitGrammaticalGender { get; set; }
    }
}
