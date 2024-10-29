using Application.Services;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection;

namespace Infrastructure.Services
{
    /// <inheritdoc cref="IEmbeddedCsvService"/>
    public class EmbeddedCsvService : IEmbeddedCsvService
    {
        /// <inheritdoc />
        public List<T> ReadEmbeddedCsv<T>(string resourceName)
        {
            var assembly = Assembly.GetCallingAssembly();
            var config = new CsvConfiguration(new CultureInfo("cs-CZ"))
            {
                Delimiter = ";",
                HasHeaderRecord = false,
                MissingFieldFound = null
            };

            using Stream? stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is null)
                return [];

            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, config);

            var records = csv.GetRecords<T>().ToList();
            return records;
        }
    }
}
