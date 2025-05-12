using Application.Common;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.AmountToWords
{
    /// <inheritdoc cref="CurrencyCzechName"/>
    public class CurrencyCzechNameModel : ModelBase
    {
        /// <inheritdoc cref="CurrencyCzechName.Code"/>
        public required string Code { get; set; }

        /// <inheritdoc cref="CurrencyCzechName.OneUnit"/>
        public required string OneUnit { get; set; }

        /// <inheritdoc cref="CurrencyCzechName.TwoToFourUnits"/>
        public required string TwoToFourUnits { get; set; }

        /// <inheritdoc cref="CurrencyCzechName.FiveOrMoreUnits"/>
        public required string FiveOrMoreUnits { get; set; }

        /// <inheritdoc cref="CurrencyCzechName.UnitGrammaticalGender"/>
        public required GrammaticalGender UnitGrammaticalGender { get; set; }

        /// <inheritdoc cref="CurrencyCzechName.OneSubunit"/>
        public required string OneSubunit { get; set; }

        /// <inheritdoc cref="CurrencyCzechName.TwoToFourSubunits"/>
        public required string TwoToFourSubunits { get; set; }

        /// <inheritdoc cref="CurrencyCzechName.FiveOrMoreSubunits"/>
        public required string FiveOrMoreSubunits { get; set; }

        /// <inheritdoc cref="CurrencyCzechName.SubunitGrammaticalGender"/>
        public required GrammaticalGender SubunitGrammaticalGender { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="CurrencyCzechNameModel"/> from a <see cref="CurrencyCzechName"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="CurrencyCzechName"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="CurrencyCzechNameModel"/>.</returns>
        public static CurrencyCzechNameModel FromEntity(CurrencyCzechName entity)
        {
            return new CurrencyCzechNameModel
            {
                Code = entity.Code,
                OneUnit = entity.OneUnit,
                TwoToFourUnits = entity.TwoToFourUnits,
                FiveOrMoreUnits = entity.FiveOrMoreUnits,
                UnitGrammaticalGender = entity.UnitGrammaticalGender,
                OneSubunit = entity.OneSubunit,
                TwoToFourSubunits = entity.TwoToFourSubunits,
                FiveOrMoreSubunits = entity.FiveOrMoreSubunits,
                SubunitGrammaticalGender = entity.SubunitGrammaticalGender
            };
        }
    }
}
