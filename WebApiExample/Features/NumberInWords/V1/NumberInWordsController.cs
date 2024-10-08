﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.Enums;
using WebApiExample.SharedServices.NumberInWords;

namespace WebApiExample.Features.NumberInWords.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class NumberInWordsController(
        INumberInWordsCzechService numberInWordsCzechService) : ControllerBase
    {
        private readonly INumberInWordsCzechService _numberInWordsCzechService = numberInWordsCzechService;

        [HttpGet("cs/number")]
        [EndpointDescription("Converts a whole number into a string representation in Czech.")]
        public IActionResult GetNumberInCzech(long number, GrammaticalGender gender, bool insertSpaces = true)
            => Ok(_numberInWordsCzechService.NumberToWords(number, gender, insertSpaces));
    }
}
