using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.NumberInWords
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmountInWordsController(
        IAmountInWordsCzechService amountInWordsCzechService) : ControllerBase
    {
        private readonly IAmountInWordsCzechService _amountInWordsCzechService = amountInWordsCzechService;

        [HttpGet("cs/amount")]
        [EndpointDescription("Converts a financial amount from a decimal number into a string representation in Czech. Supported currency codes: CZK, EUR, USD")]
        public async Task<IActionResult> GetAmountInCzechAsync(decimal amount, string currencyCode, bool insertSpacesIntoNumbers = true)
        {
            string result = await _amountInWordsCzechService.AmountToWordsAsync(amount, currencyCode, insertSpacesIntoNumbers);

            if (result == string.Empty)
                return BadRequest("Invalid or unsupported currency code.");

            return Ok(result);
        }
    }
}
