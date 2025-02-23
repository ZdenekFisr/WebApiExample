using Application.Features.PrimeNumbers;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.PrimeNumbers.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/primes")]
    [ApiController]
    public class PrimesController(IPrimeNumbersService coreService) : ControllerBase
    {
        private readonly IPrimeNumbersService _coreService = coreService;

        [HttpGet("decompose")]
        [EndpointDescription("Accepts a whole number and returns a collection of primes whose product is the input number.")]
        public IActionResult DecomposeIntoPrimes(long input)
            => Ok(_coreService.DecomposeIntoPrimes(input));

        [HttpGet("most-primes")]
        [EndpointDescription("Accepts a range of whole numbers and returns a number which can be decomposed into the largest number of primes within that range.")]
        public IActionResult FindNumberWithMostPrimes(long leftBound, long rightBound)
            => Ok(_coreService.FindNumberWithMostPrimes(leftBound, rightBound));

        [HttpGet("prime-gaps")]
        [EndpointDescription("Accepts a range of whole numbers, then finds all primes within that range, counts gaps between all pairs of neighbouring primes by size and returns results.")]
        public IActionResult CountGapsBetweenPrimes(long leftBound, long rightBound)
        {
            var result = _coreService.CountGapsBetweenPrimes(leftBound, rightBound);

            if (result is null)
                return BadRequest("Right bound must be greater than left bound.");

            return Ok(result);
        }
    }
}
