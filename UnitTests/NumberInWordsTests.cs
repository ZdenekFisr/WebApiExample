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
            => PerformNumberToWordsTest("dv�", 2, GrammaticalGender.Feminine);

        [TestMethod]
        public void NumberToWords_TwoNeuter()
            => PerformNumberToWordsTest("dv�", 2, GrammaticalGender.Neuter);

        [TestMethod]
        public void NumberToWords_Three()
            => PerformNumberToWordsTest("t�i", 3, GrammaticalGender.Masculine);

        [TestMethod]
        public void NumberToWords_Twenty()
            => PerformNumberToWordsTest("dvacet", 20, GrammaticalGender.Feminine);

        [TestMethod]
        public void NumberToWords_ThirtyOne()
            => PerformNumberToWordsTest("t�icet jedno", 31, GrammaticalGender.Neuter);


        [TestMethod]
        public void NumberToWords_Hundred()
            => PerformNumberToWordsTest("sto", 100, GrammaticalGender.Masculine);

        [TestMethod]
        public void NumberToWords_HundredComplex()
            => PerformNumberToWordsTest("dv� st� t�icet dv�", 232, GrammaticalGender.Feminine);


        [TestMethod]
        public void NumberToWords_Thousand()
            => PerformNumberToWordsTest("tis�c", 1000, GrammaticalGender.Neuter);

        [TestMethod]
        public void NumberToWords_TwoThousand()
            => PerformNumberToWordsTest("dva tis�ce", 2000, GrammaticalGender.Masculine);

        [TestMethod]
        public void NumberToWords_FiveThousand()
            => PerformNumberToWordsTest("p�t tis�c", 5000, GrammaticalGender.Feminine);

        [TestMethod]
        public void NumberToWords_Million()
            => PerformNumberToWordsTest("milion", 1000000, GrammaticalGender.Neuter);

        [TestMethod]
        public void NumberToWords_MillionComplex()
            => PerformNumberToWordsTest("milion dv� st� t�icet �ty�i tis�ce p�t set �edes�t sedm", 1234567, GrammaticalGender.Feminine);

        [TestMethod]
        public void NumberToWords_LargeNumberWithUnits()
            => PerformNumberToWordsTest("trilion biliarda bilion miliarda milion tis�c jedna", 1001001001001001001, GrammaticalGender.Feminine);

        [TestMethod]
        public void NumberToWords_LargeNumberWithTwoToFour()
            => PerformNumberToWordsTest("dva triliony t�i biliardy �ty�i biliony �ty�icet dv� miliardy p�t set �edes�t t�i miliony �ty�i tis�ce dva", 2003004042563004002, GrammaticalGender.Masculine);

        [TestMethod]
        public void NumberToWords_LargeNumberWithSkippedParts()
            => PerformNumberToWordsTest("dva triliony osmdes�t �ty�i biliony sto dev�t milion�", 2000084000109000000, GrammaticalGender.Neuter);

        [TestMethod]
        public void NumberToWords_LargeNumberWithComplexParts()
            => PerformNumberToWordsTest("p�t trilion� sto dvacet jedna biliarda dv� st� t�icet jeden bilion t�i sta �ty�icet sedm miliard �ty�i sta pades�t dev�t milion� osm set t�icet �est tis�c sedm set devades�t jeden", 5121231347459836791, GrammaticalGender.Masculine);

        [TestMethod]
        public void NumberToWords_LargeNumberWithComplexPartsNoSpaces()
            => PerformNumberToWordsTest("p�ttrilion�stodvacetjednabiliardadv�st�t�icetjedenbiliont�ista�ty�icetsedmmiliard�ty�istapades�tdev�tmilion�osmsett�icet�esttis�csedmsetdevades�tjeden", 5121231347459836791, GrammaticalGender.Masculine, false);


        [TestMethod]
        public void NumberToWords_Negative()
            => PerformNumberToWordsTest("minus �ty�icet dva", -42, GrammaticalGender.Masculine);
    }
}