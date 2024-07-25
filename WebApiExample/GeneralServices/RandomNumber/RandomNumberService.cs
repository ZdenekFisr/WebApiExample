namespace WebApiExample.GeneralServices.RandomNumber
{
    /// <inheritdoc cref="IRandomNumberService"/>
    public class RandomNumberService : IRandomNumberService
    {
        private readonly Random _random = new();

        /// <inheritdoc />
        public int GenerateRandomInteger(int leftBound, int rightBound)
            => _random.Next(leftBound, rightBound + 1);
    }
}
