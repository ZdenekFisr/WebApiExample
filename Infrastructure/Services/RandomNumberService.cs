using Application.Services;

namespace Infrastructure.Services
{
    /// <inheritdoc cref="IRandomNumberService"/>
    public class RandomNumberService(Random random) : IRandomNumberService
    {
        private readonly Random _random = random;

        /// <inheritdoc />
        public int GenerateRandomInteger(int leftBound, int rightBound)
            => _random.Next(leftBound, rightBound + 1);
    }
}
