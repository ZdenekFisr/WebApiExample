namespace WebApiExample.GeneralServices.RandomNumber
{
    /// <summary>
    /// Contains a method for the generation of a random integer.
    /// </summary>
    public interface IRandomNumberService
    {
        /// <summary>
        /// Generates a random integer within a specified range.
        /// </summary>
        /// <param name="leftBound">The lowest number of the input range.</param>
        /// <param name="rightBound">The highest number of the input range.</param>
        /// <returns>Random integer.</returns>
        public int GenerateRandomInteger(int leftBound, int rightBound);
    }
}
