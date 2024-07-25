using Microsoft.Extensions.DependencyInjection;
using WebApiExample.Enums;
using WebApiExample.Features.NumberInWords;

namespace UnitTests
{
    [TestClass]
    public class NumberInWordsTests
    {
        private readonly ServiceProvider _serviceProvider;

        public NumberInWordsTests()
        {
            var services = new ServiceCollection();
            services.AddTransient<INumberInWordsCzechService, NumberInWordsCzechService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [TestMethod]
        public void NumberToWords_Zero()
            => Assert.AreEqual("nula", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(0, GrammaticalGender.Masculine));


        [TestMethod]
        public void NumberToWords_OneMasculine()
            => Assert.AreEqual("jeden", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(1, GrammaticalGender.Masculine));

        [TestMethod]
        public void NumberToWords_OneFeminine()
            => Assert.AreEqual("jedna", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(1, GrammaticalGender.Feminine));

        [TestMethod]
        public void NumberToWords_OneNeuter()
            => Assert.AreEqual("jedno", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(1, GrammaticalGender.Neuter));

        [TestMethod]
        public void NumberToWords_TwoMasculine()
            => Assert.AreEqual("dva", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(2, GrammaticalGender.Masculine));

        [TestMethod]
        public void NumberToWords_TwoFeminine()
            => Assert.AreEqual("dv�", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(2, GrammaticalGender.Feminine));

        [TestMethod]
        public void NumberToWords_TwoNeuter()
            => Assert.AreEqual("dv�", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(2, GrammaticalGender.Neuter));

        [TestMethod]
        public void NumberToWords_Three()
            => Assert.AreEqual("t�i", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(3, GrammaticalGender.Masculine));

        [TestMethod]
        public void NumberToWords_Twenty()
            => Assert.AreEqual("dvacet", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(20, GrammaticalGender.Feminine));

        [TestMethod]
        public void NumberToWords_ThirtyOne()
            => Assert.AreEqual("t�icet jedno", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(31, GrammaticalGender.Neuter));


        [TestMethod]
        public void NumberToWords_Hundred()
            => Assert.AreEqual("sto", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(100, GrammaticalGender.Masculine));

        [TestMethod]
        public void NumberToWords_HundredComplex()
            => Assert.AreEqual("dv� st� t�icet dv�", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(232, GrammaticalGender.Feminine));


        [TestMethod]
        public void NumberToWords_Thousand()
            => Assert.AreEqual("tis�c", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(1000, GrammaticalGender.Neuter));

        [TestMethod]
        public void NumberToWords_TwoThousand()
            => Assert.AreEqual("dva tis�ce", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(2000, GrammaticalGender.Masculine));

        [TestMethod]
        public void NumberToWords_FiveThousand()
            => Assert.AreEqual("p�t tis�c", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(5000, GrammaticalGender.Feminine));

        [TestMethod]
        public void NumberToWords_Million()
            => Assert.AreEqual("milion", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(1000000, GrammaticalGender.Neuter));

        [TestMethod]
        public void NumberToWords_MillionComplex()
            => Assert.AreEqual("milion dv� st� t�icet �ty�i tis�ce p�t set �edes�t sedm", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(1234567, GrammaticalGender.Feminine));

        [TestMethod]
        public void NumberToWords_LargeNumberWithUnits()
            => Assert.AreEqual("trilion biliarda bilion miliarda milion tis�c jedna", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(1001001001001001001, GrammaticalGender.Feminine));

        [TestMethod]
        public void NumberToWords_LargeNumberWithTwoToFour()
            => Assert.AreEqual("dva triliony t�i biliardy �ty�i biliony �ty�icet dv� miliardy p�t set �edes�t t�i miliony �ty�i tis�ce dva", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(2003004042563004002, GrammaticalGender.Masculine));

        [TestMethod]
        public void NumberToWords_LargeNumberWithSkippedParts()
            => Assert.AreEqual("dva triliony osmdes�t �ty�i biliony sto dev�t milion�", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(2000084000109000000, GrammaticalGender.Neuter));

        [TestMethod]
        public void NumberToWords_LargeNumberWithComplexParts()
            => Assert.AreEqual("p�t trilion� sto dvacet jedna biliarda dv� st� t�icet jeden bilion t�i sta �ty�icet sedm miliard �ty�i sta pades�t dev�t milion� osm set t�icet �est tis�c sedm set devades�t jeden", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(5121231347459836791, GrammaticalGender.Masculine));
        
        [TestMethod]
        public void NumberToWords_LargeNumberWithComplexPartsNoSpaces()
            => Assert.AreEqual("p�ttrilion�stodvacetjednabiliardadv�st�t�icetjedenbiliont�ista�ty�icetsedmmiliard�ty�istapades�tdev�tmilion�osmsett�icet�esttis�csedmsetdevades�tjeden", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(5121231347459836791, GrammaticalGender.Masculine, false));


        [TestMethod]
        public void NumberToWords_Negative()
            => Assert.AreEqual("minus �ty�icet dva", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(-42, GrammaticalGender.Masculine));
    }
}