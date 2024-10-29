namespace Application.Features.AmountToWords
{
    /// <summary>
    /// Repository that retrieves Czech names of currencies from a DB.
    /// </summary>
    public interface ICurrencyCzechNameRepository
    {
        /// <summary>
        /// Retrieves Czech names of currencies from a DB.
        /// </summary>
        /// <param name="currencyCode">International currency code (case-insensitive).</param>
        /// <returns>Czech names of currencies.</returns>
        Task<CurrencyCzechNameModel?> GetCurrencyCzechNameByCodeAsync(string currencyCode);
    }
}
