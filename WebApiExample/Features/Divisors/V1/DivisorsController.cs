using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.Divisors.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DivisorsController(IDivisorsService coreService) : ControllerBase
    {
        private readonly IDivisorsService _coreService = coreService;

        [HttpGet]
        [EndpointDescription("Accepts a whole number and returns a collection of its divisors.")]
        public IActionResult GetDivisors(long number)
            => Ok(_coreService.GetDivisors(number));

        [HttpGet("most-divisors")]
        [EndpointDescription("Accepts a range of whole numbers and returns a number with the most divisors (list of its divisors included) within that range.")]
        public IActionResult FindNumberWithMostDivisors(long leftBound, long rightBound)
            => Ok(_coreService.FindNumberWithMostDivisors(leftBound, rightBound));
    }
}
