using Domain.Enums;

namespace Application.Features.NumberToWords
{
    /// <summary>
    /// Contains a method for converting a whole number into a string representation in Czech.
    /// </summary>
    public interface INumberToWordsCzechService
    {
        /// <summary>
        /// Converts a whole number into a string representation in Czech.
        /// </summary>
        /// <param name="number">Whole number.</param>
        /// <param name="gender">Czech grammatical gender.</param>
        /// <param name="insertSpaces">Insert spaces into the result string.</param>
        /// <returns>String representation in Czech.</returns>
        public string NumberToWords(long number, GrammaticalGender gender, bool insertSpaces = true);
    }
}
