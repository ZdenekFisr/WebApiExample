using FluentAssertions;
using Infrastructure.Services;

namespace Infrastructure.UnitTests.ServicesTests.EmbeddedCsv
{
    public class EmbeddedCsvServiceTests
    {
        private readonly EmbeddedCsvService _service;

        public EmbeddedCsvServiceTests()
        {
            _service = new();
        }

        [Fact]
        public void ReadEmbeddedCsv()
        {
            TestCsvRecord[] expected = [
                new() { Id = 0, Name = "A", Value = 0.5 },
                new() { Id = 1, Name = "B", Value = -0.5 },
                new() { Id = 2, Name = "C", Value = 1 }
            ];

            List<TestCsvRecord> actual = _service.ReadEmbeddedCsv<TestCsvRecord>("Infrastructure.UnitTests.ServicesTests.EmbeddedCsv.TestCsvRecords.csv");

            actual.Should().BeEquivalentTo(expected, options => options
                .WithStrictOrdering()
                .IncludingAllDeclaredProperties());
        }
    }

    file class TestCsvRecord
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public double Value { get; set; }
    }
}
