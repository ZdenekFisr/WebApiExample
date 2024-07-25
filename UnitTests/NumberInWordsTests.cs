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
            => Assert.AreEqual("dvì", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(2, GrammaticalGender.Feminine));

        [TestMethod]
        public void NumberToWords_TwoNeuter()
            => Assert.AreEqual("dvì", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(2, GrammaticalGender.Neuter));

        [TestMethod]
        public void NumberToWords_Three()
            => Assert.AreEqual("tøi", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(3, GrammaticalGender.Masculine));

        [TestMethod]
        public void NumberToWords_Twenty()
            => Assert.AreEqual("dvacet", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(20, GrammaticalGender.Feminine));

        [TestMethod]
        public void NumberToWords_ThirtyOne()
            => Assert.AreEqual("tøicet jedno", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(31, GrammaticalGender.Neuter));


        [TestMethod]
        public void NumberToWords_Hundred()
            => Assert.AreEqual("sto", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(100, GrammaticalGender.Masculine));

        [TestMethod]
        public void NumberToWords_HundredComplex()
            => Assert.AreEqual("dvì stì tøicet dvì", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(232, GrammaticalGender.Feminine));


        [TestMethod]
        public void NumberToWords_Thousand()
            => Assert.AreEqual("tisíc", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(1000, GrammaticalGender.Neuter));

        [TestMethod]
        public void NumberToWords_TwoThousand()
            => Assert.AreEqual("dva tisíce", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(2000, GrammaticalGender.Masculine));

        [TestMethod]
        public void NumberToWords_FiveThousand()
            => Assert.AreEqual("pìt tisíc", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(5000, GrammaticalGender.Feminine));

        [TestMethod]
        public void NumberToWords_Million()
            => Assert.AreEqual("milion", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(1000000, GrammaticalGender.Neuter));

        [TestMethod]
        public void NumberToWords_MillionComplex()
            => Assert.AreEqual("milion dvì stì tøicet ètyøi tisíce pìt set šedesát sedm", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(1234567, GrammaticalGender.Feminine));

        [TestMethod]
        public void NumberToWords_LargeNumberWithUnits()
            => Assert.AreEqual("trilion biliarda bilion miliarda milion tisíc jedna", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(1001001001001001001, GrammaticalGender.Feminine));

        [TestMethod]
        public void NumberToWords_LargeNumberWithTwoToFour()
            => Assert.AreEqual("dva triliony tøi biliardy ètyøi biliony ètyøicet dvì miliardy pìt set šedesát tøi miliony ètyøi tisíce dva", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(2003004042563004002, GrammaticalGender.Masculine));

        [TestMethod]
        public void NumberToWords_LargeNumberWithSkippedParts()
            => Assert.AreEqual("dva triliony osmdesát ètyøi biliony sto devìt milionù", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(2000084000109000000, GrammaticalGender.Neuter));

        [TestMethod]
        public void NumberToWords_LargeNumberWithComplexParts()
            => Assert.AreEqual("pìt trilionù sto dvacet jedna biliarda dvì stì tøicet jeden bilion tøi sta ètyøicet sedm miliard ètyøi sta padesát devìt milionù osm set tøicet šest tisíc sedm set devadesát jeden", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(5121231347459836791, GrammaticalGender.Masculine));
        
        [TestMethod]
        public void NumberToWords_LargeNumberWithComplexPartsNoSpaces()
            => Assert.AreEqual("pìttrilionùstodvacetjednabiliardadvìstìtøicetjedenbiliontøistaètyøicetsedmmiliardètyøistapadesátdevìtmilionùosmsettøicetšesttisícsedmsetdevadesátjeden", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(5121231347459836791, GrammaticalGender.Masculine, false));


        [TestMethod]
        public void NumberToWords_Negative()
            => Assert.AreEqual("minus ètyøicet dva", _serviceProvider.GetService<INumberInWordsCzechService>().NumberToWords(-42, GrammaticalGender.Masculine));
    }
}