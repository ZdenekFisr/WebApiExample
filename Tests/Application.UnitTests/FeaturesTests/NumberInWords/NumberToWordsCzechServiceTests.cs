using Application.Features.NumberToWords;
using Domain.Enums;
using FluentAssertions;

namespace Application.UnitTests.FeaturesTests.NumberInWords
{
    public class NumberToWordsCzechServiceTests
    {
        private readonly NumberToWordsCzechService _service;

        public NumberToWordsCzechServiceTests()
        {
            _service = new();
        }

        [Theory]
        [InlineData("nula", 0, GrammaticalGender.Masculine)]
        [InlineData("jeden", 1, GrammaticalGender.Masculine)]
        [InlineData("jedna", 1, GrammaticalGender.Feminine)]
        [InlineData("jedno", 1, GrammaticalGender.Neuter)]
        [InlineData("dva", 2, GrammaticalGender.Masculine)]
        [InlineData("dvě", 2, GrammaticalGender.Feminine)]
        [InlineData("dvě", 2, GrammaticalGender.Neuter)]
        [InlineData("tři", 3, GrammaticalGender.Masculine)]
        [InlineData("dvacet", 20, GrammaticalGender.Feminine)]
        [InlineData("třicet jedno", 31, GrammaticalGender.Neuter)]
        [InlineData("sto", 100, GrammaticalGender.Masculine)]
        [InlineData("dvě stě třicet dvě", 232, GrammaticalGender.Feminine)]
        [InlineData("tisíc", 1_000, GrammaticalGender.Neuter)]
        [InlineData("dva tisíce", 2_000, GrammaticalGender.Masculine)]
        [InlineData("pět tisíc", 5_000, GrammaticalGender.Feminine)]
        [InlineData("milion", 1_000_000, GrammaticalGender.Neuter)]
        [InlineData("milion dvě stě třicet čtyři tisíce pět set šedesát sedm", 1_234_567, GrammaticalGender.Feminine)]
        [InlineData("trilion biliarda bilion miliarda milion tisíc jedna", 1_001_001_001_001_001_001, GrammaticalGender.Feminine)]
        [InlineData("dva triliony tři biliardy čtyři biliony čtyřicet dvě miliardy pět set šedesát tři miliony čtyři tisíce dva", 2_003_004_042_563_004_002, GrammaticalGender.Masculine)]
        [InlineData("dva triliony osmdesát čtyři biliony sto devět milionů", 2_000_084_000_109_000_000, GrammaticalGender.Neuter)]
        [InlineData("pět trilionů sto dvacet jedna biliarda dvě stě třicet jeden bilion tři sta čtyřicet sedm miliard čtyři sta padesát devět milionů osm set třicet šest tisíc sedm set devadesát jeden", 5_121_231_347_459_836_791, GrammaticalGender.Masculine)]
        [InlineData("pěttrilionůstodvacetjednabiliardadvěstětřicetjedenbiliontřistačtyřicetsedmmiliardčtyřistapadesátdevětmilionůosmsettřicetšesttisícsedmsetdevadesátjeden", 5_121_231_347_459_836_791, GrammaticalGender.Masculine, false)]
        [InlineData("minus čtyřicet dva", -42, GrammaticalGender.Masculine)]
        public void NumberToWords_ReturnsCorrectStringRepresentation(string expected, long number, GrammaticalGender grammaticalGender, bool insertSpaces = true)
        {
            string actual = _service.NumberToWords(number, grammaticalGender, insertSpaces);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
