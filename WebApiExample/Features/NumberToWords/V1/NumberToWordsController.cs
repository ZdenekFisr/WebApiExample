using Application.Features.NumberToWords;
using Asp.Versioning;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.NumberToWords.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class NumberToWordsController(
        INumberToWordsCzechService numberToWordsCzechService) : ControllerBase
    {
        private readonly INumberToWordsCzechService _numberToWordsCzechService = numberToWordsCzechService;

        [HttpGet("cs/number")]
        [EndpointDescription("Converts a whole number into a string representation in Czech.")]
        public IActionResult GetNumberInCzech(long number, GrammaticalGender gender, bool insertSpaces = true)
            => Ok(_numberToWordsCzechService.NumberToWords(number, gender, insertSpaces));
    }
}
