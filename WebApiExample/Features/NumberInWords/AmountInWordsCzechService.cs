using System.Text;
using WebApiExample.Extensions;

namespace WebApiExample.Features.NumberInWords
{
    /// <inheritdoc cref="IAmountInWordsCzechService"/>
    public class AmountInWordsCzechService(
        INumberInWordsCzechService numberInWordsCzechService,
        ICurrencyCzechNameRepository currencyCzechNameRepository)
        : IAmountInWordsCzechService
    {
        private readonly INumberInWordsCzechService _numberInWordsCzechService = numberInWordsCzechService;
        private readonly ICurrencyCzechNameRepository _currencyCzechNameRepository = currencyCzechNameRepository;

        /// <inheritdoc />
        public async Task<string> AmountToWordsAsync(decimal amount, string currencyCode, bool insertSpacesIntoNumbers)
        {
            CurrencyCzechName? currency = await GetCurrencyCzechNameAsync(currencyCode);

            if (currency is null)
                return string.Empty;

            StringBuilder resultBuilder = new();
            long wholeUnits = (long)Math.Truncate(amount);
            resultBuilder
                .Append(_numberInWordsCzechService.NumberToWords(wholeUnits, currency.UnitGrammaticalGender, insertSpacesIntoNumbers));

            AppendCurrencyName(currency.OneUnit, currency.TwoToFourUnits, currency.FiveOrMoreUnits, resultBuilder, wholeUnits);

            if (!amount.IsWholeNumber())
            {
                byte subunits = (byte)(Math.Abs(amount - wholeUnits) * 100);
                resultBuilder
                    .Append(' ')
                    .Append(_numberInWordsCzechService.NumberToWords(subunits, currency.SubunitGrammaticalGender, insertSpacesIntoNumbers));

                AppendCurrencyName(currency.OneSubunit, currency.TwoToFourSubunits, currency.FiveOrMoreSubunits, resultBuilder, subunits);
            }

            return resultBuilder.ToString();
        }

        private async Task<CurrencyCzechName?> GetCurrencyCzechNameAsync(string currencyCode)
            => await _currencyCzechNameRepository.GetCurrencyCzechNameByCodeAsync(currencyCode);

        private void AppendCurrencyName(string oneUnit, string twoToFourUnits, string fiveOrMoreUnits, StringBuilder resultBuilder, long unitsCount)
        {
            resultBuilder.Append(' ');
            unitsCount = Math.Abs(unitsCount);

            if (unitsCount % 10 == 1 && unitsCount % 100 != 11)
                resultBuilder.Append(oneUnit);
            else if (unitsCount % 10 >= 2 && unitsCount % 10 <= 4 && (unitsCount % 100 < 12 || unitsCount % 100 > 14))
                resultBuilder.Append(twoToFourUnits);
            else
                resultBuilder.Append(fiveOrMoreUnits);
        }
    }
}
