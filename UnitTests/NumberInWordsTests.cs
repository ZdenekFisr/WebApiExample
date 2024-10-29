using Application.Features.NumberToWords;
using Domain.Enums;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests
{
    [TestClass]
    public class NumberInWordsTests
    {
        private ServiceProvider _serviceProvider;
        private IServiceScope _serviceScope;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddTransient<INumberToWordsCzechService, NumberToWordsCzechService>();

            _serviceProvider = services.BuildServiceProvider();
            _serviceScope = _serviceProvider.CreateScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceScope.Dispose();
            _serviceProvider.Dispose();
        }

        private void PerformNumberToWordsTest(string expected, long number, GrammaticalGender grammaticalGender, bool insertSpaces = true)
        {
            string actual = _serviceProvider.GetRequiredService<INumberToWordsCzechService>().NumberToWords(number, grammaticalGender, insertSpaces);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void NumberToWords_Zero()
            => PerformNumberToWordsTest("nula", 0, GrammaticalGender.Masculine);


        [TestMethod]
        public void NumberToWords_OneMasculine()
            => PerformNumberToWordsTest("jeden", 1, GrammaticalGender.Masculine);

        [TestMethod]
        public void NumberToWords_OneFeminine()
            => PerformNumberToWordsTest("jedna", 1, GrammaticalGender.Feminine);

        [TestMethod]
        public void NumberToWords_OneNeuter()
            => PerformNumberToWordsTest("jedno", 1, GrammaticalGender.Neuter);

        [TestMethod]
        public void NumberToWords_TwoMasculine()
            => PerformNumberToWordsTest("dva", 2, GrammaticalGender.Masculine);

        [TestMethod]
        public void NumberToWords_TwoFeminine()
            => PerformNumberToWordsTest("dvì", 2, GrammaticalGender.Feminine);

        [TestMethod]
        public void NumberToWords_TwoNeuter()
            => PerformNumberToWordsTest("dvì", 2, GrammaticalGender.Neuter);

        [TestMethod]
        public void NumberToWords_Three()
            => PerformNumberToWordsTest("tøi", 3, GrammaticalGender.Masculine);

        [TestMethod]
        public void NumberToWords_Twenty()
            => PerformNumberToWordsTest("dvacet", 20, GrammaticalGender.Feminine);

        [TestMethod]
        public void NumberToWords_ThirtyOne()
            => PerformNumberToWordsTest("tøicet jedno", 31, GrammaticalGender.Neuter);


        [TestMethod]
        public void NumberToWords_Hundred()
            => PerformNumberToWordsTest("sto", 100, GrammaticalGender.Masculine);

        [TestMethod]
        public void NumberToWords_HundredComplex()
            => PerformNumberToWordsTest("dvì stì tøicet dvì", 232, GrammaticalGender.Feminine);


        [TestMethod]
        public void NumberToWords_Thousand()
            => PerformNumberToWordsTest("tisíc", 1000, GrammaticalGender.Neuter);

        [TestMethod]
        public void NumberToWords_TwoThousand()
            => PerformNumberToWordsTest("dva tisíce", 2000, GrammaticalGender.Masculine);

        [TestMethod]
        public void NumberToWords_FiveThousand()
            => PerformNumberToWordsTest("pìt tisíc", 5000, GrammaticalGender.Feminine);

        [TestMethod]
        public void NumberToWords_Million()
            => PerformNumberToWordsTest("milion", 1000000, GrammaticalGender.Neuter);

        [TestMethod]
        public void NumberToWords_MillionComplex()
            => PerformNumberToWordsTest("milion dvì stì tøicet ètyøi tisíce pìt set šedesát sedm", 1234567, GrammaticalGender.Feminine);

        [TestMethod]
        public void NumberToWords_LargeNumberWithUnits()
            => PerformNumberToWordsTest("trilion biliarda bilion miliarda milion tisíc jedna", 1001001001001001001, GrammaticalGender.Feminine);

        [TestMethod]
        public void NumberToWords_LargeNumberWithTwoToFour()
            => PerformNumberToWordsTest("dva triliony tøi biliardy ètyøi biliony ètyøicet dvì miliardy pìt set šedesát tøi miliony ètyøi tisíce dva", 2003004042563004002, GrammaticalGender.Masculine);

        [TestMethod]
        public void NumberToWords_LargeNumberWithSkippedParts()
            => PerformNumberToWordsTest("dva triliony osmdesát ètyøi biliony sto devìt milionù", 2000084000109000000, GrammaticalGender.Neuter);

        [TestMethod]
        public void NumberToWords_LargeNumberWithComplexParts()
            => PerformNumberToWordsTest("pìt trilionù sto dvacet jedna biliarda dvì stì tøicet jeden bilion tøi sta ètyøicet sedm miliard ètyøi sta padesát devìt milionù osm set tøicet šest tisíc sedm set devadesát jeden", 5121231347459836791, GrammaticalGender.Masculine);

        [TestMethod]
        public void NumberToWords_LargeNumberWithComplexPartsNoSpaces()
            => PerformNumberToWordsTest("pìttrilionùstodvacetjednabiliardadvìstìtøicetjedenbiliontøistaètyøicetsedmmiliardètyøistapadesátdevìtmilionùosmsettøicetšesttisícsedmsetdevadesátjeden", 5121231347459836791, GrammaticalGender.Masculine, false);


        [TestMethod]
        public void NumberToWords_Negative()
            => PerformNumberToWordsTest("minus ètyøicet dva", -42, GrammaticalGender.Masculine);
    }
}