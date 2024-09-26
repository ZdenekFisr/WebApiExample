namespace WebApiExample.SharedServices.Csv
{
    public interface ICsvService
    {
        List<T> ReadEmbeddedCsv<T>(string resourceName);
    }
}
