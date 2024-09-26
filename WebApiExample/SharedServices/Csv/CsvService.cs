using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection;

namespace WebApiExample.SharedServices.Csv
{
    public class CsvService : ICsvService
    {
        public List<T> ReadEmbeddedCsv<T>(string resourceName)
        {
            var assembly = Assembly.GetCallingAssembly();
            var config = new CsvConfiguration(CultureInfo.CurrentCulture)
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
