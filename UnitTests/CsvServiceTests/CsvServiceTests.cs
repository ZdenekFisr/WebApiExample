using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WebApiExample.SharedServices.Csv;

namespace UnitTests.CsvServiceTests
{
    [TestClass]
    public class CsvServiceTests
    {
        private ServiceProvider _serviceProvider;
        private IServiceScope _serviceScope;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddScoped<ICsvService, CsvService>();

            _serviceProvider = services.BuildServiceProvider();
            _serviceScope = _serviceProvider.CreateScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceScope.Dispose();
            _serviceProvider.Dispose();
        }

        [TestMethod]
        public void ReadEmbeddedCsv()
        {
            CsvRecord[] expected = [
                new() { Id = 0, Name = "A", Value = 0.5 },
                new() { Id = 1, Name = "B", Value = -0.5 },
                new() { Id = 2, Name = "C", Value = 1 }
            ];

            List<CsvRecord> actual = _serviceProvider.GetRequiredService<ICsvService>().ReadEmbeddedCsv<CsvRecord>("UnitTests.CsvServiceTests.TestCsvRecords.csv");

            actual.Should().BeEquivalentTo(expected, options => options
                .WithStrictOrdering()
                .IncludingAllDeclaredProperties());
        }
    }

    file class CsvRecord
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public double Value { get; set; }
    }
}
