namespace WebApiExample.Features.AmountInWords
{
    /// <summary>
    /// Contains a method for converting a financial amount into a string representation in Czech.
    /// </summary>
    public interface IAmountInWordsCzechService
    {
        /// <summary>
        /// Converts a financial amount into a string representation in Czech.
        /// </summary>
        /// <param name="number">Financial amount.</param>
        /// <param name="currencyCode">International currency code (case-insensitive).</param>
        /// <param name="insertSpacesIntoNumbers">Insert space into the number parts of the result string.</param>
        /// <returns>String representation in Czech.</returns>
        Task<string> AmountToWordsAsync(decimal amount, string currencyCode, bool insertSpacesIntoNumbers = true);
    }
}
