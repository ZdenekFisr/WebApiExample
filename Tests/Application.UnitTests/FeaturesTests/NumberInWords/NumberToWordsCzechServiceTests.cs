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

        private void PerformNumberToWordsTest(string expected, long number, GrammaticalGender grammaticalGender, bool insertSpaces = true)
        {
            string actual = _service.NumberToWords(number, grammaticalGender, insertSpaces);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void NumberToWords_Zero()
            => PerformNumberToWordsTest("nula", 0, GrammaticalGender.Masculine);


        [Fact]
        public void NumberToWords_OneMasculine()
            => PerformNumberToWordsTest("jeden", 1, GrammaticalGender.Masculine);

        [Fact]
        public void NumberToWords_OneFeminine()
            => PerformNumberToWordsTest("jedna", 1, GrammaticalGender.Feminine);

        [Fact]
        public void NumberToWords_OneNeuter()
            => PerformNumberToWordsTest("jedno", 1, GrammaticalGender.Neuter);

        [Fact]
        public void NumberToWords_TwoMasculine()
            => PerformNumberToWordsTest("dva", 2, GrammaticalGender.Masculine);

        [Fact]
        public void NumberToWords_TwoFeminine()
            => PerformNumberToWordsTest("dvě", 2, GrammaticalGender.Feminine);

        [Fact]
        public void NumberToWords_TwoNeuter()
            => PerformNumberToWordsTest("dvě", 2, GrammaticalGender.Neuter);

        [Fact]
        public void NumberToWords_Three()
            => PerformNumberToWordsTest("tři", 3, GrammaticalGender.Masculine);

        [Fact]
        public void NumberToWords_Twenty()
            => PerformNumberToWordsTest("dvacet", 20, GrammaticalGender.Feminine);

        [Fact]
        public void NumberToWords_ThirtyOne()
            => PerformNumberToWordsTest("třicet jedno", 31, GrammaticalGender.Neuter);


        [Fact]
        public void NumberToWords_Hundred()
            => PerformNumberToWordsTest("sto", 100, GrammaticalGender.Masculine);

        [Fact]
        public void NumberToWords_HundredComplex()
            => PerformNumberToWordsTest("dvě stě třicet dvě", 232, GrammaticalGender.Feminine);


        [Fact]
        public void NumberToWords_Thousand()
            => PerformNumberToWordsTest("tisíc", 1000, GrammaticalGender.Neuter);

        [Fact]
        public void NumberToWords_TwoThousand()
            => PerformNumberToWordsTest("dva tisíce", 2000, GrammaticalGender.Masculine);

        [Fact]
        public void NumberToWords_FiveThousand()
            => PerformNumberToWordsTest("pět tisíc", 5000, GrammaticalGender.Feminine);

        [Fact]
        public void NumberToWords_Million()
            => PerformNumberToWordsTest("milion", 1000000, GrammaticalGender.Neuter);

        [Fact]
        public void NumberToWords_MillionComplex()
            => PerformNumberToWordsTest("milion dvě stě třicet čtyři tisíce pět set šedesát sedm", 1234567, GrammaticalGender.Feminine);

        [Fact]
        public void NumberToWords_LargeNumberWithUnits()
            => PerformNumberToWordsTest("trilion biliarda bilion miliarda milion tisíc jedna", 1001001001001001001, GrammaticalGender.Feminine);

        [Fact]
        public void NumberToWords_LargeNumberWithTwoToFour()
            => PerformNumberToWordsTest("dva triliony tři biliardy čtyři biliony čtyřicet dvě miliardy pět set šedesát tři miliony čtyři tisíce dva", 2003004042563004002, GrammaticalGender.Masculine);

        [Fact]
        public void NumberToWords_LargeNumberWithSkippedParts()
            => PerformNumberToWordsTest("dva triliony osmdesát čtyři biliony sto devět milionů", 2000084000109000000, GrammaticalGender.Neuter);

        [Fact]
        public void NumberToWords_LargeNumberWithComplexParts()
            => PerformNumberToWordsTest("pět trilionů sto dvacet jedna biliarda dvě stě třicet jeden bilion tři sta čtyřicet sedm miliard čtyři sta padesát devět milionů osm set třicet šest tisíc sedm set devadesát jeden", 5121231347459836791, GrammaticalGender.Masculine);

        [Fact]
        public void NumberToWords_LargeNumberWithComplexPartsNoSpaces()
            => PerformNumberToWordsTest("pěttrilionůstodvacetjednabiliardadvěstětřicetjedenbiliontřistačtyřicetsedmmiliardčtyřistapadesátdevětmilionůosmsettřicetšesttisícsedmsetdevadesátjeden", 5121231347459836791, GrammaticalGender.Masculine, false);


        [Fact]
        public void NumberToWords_Negative()
            => PerformNumberToWordsTest("minus čtyřicet dva", -42, GrammaticalGender.Masculine);
    }
}
