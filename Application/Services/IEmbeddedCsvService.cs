namespace Application.Services
{
    /// <summary>
    /// Contains method to process CSV files.
    /// </summary>
    public interface IEmbeddedCsvService
    {
        /// <summary>
        /// <para>Reads a CSV file stored as an embedded resource in the source code and deserializes it into a list of objects.</para>
        /// <para>Note: Decimal points must be written as commas.</para>
        /// </summary>
        /// <typeparam name="T">Type of object into which the file is deserialized.</typeparam>
        /// <param name="resourceName">Namespace of the file.</param>
        /// <returns>A <see cref="List{T}"/> of corresponding objects.</returns>
        List<T> ReadEmbeddedCsv<T>(string resourceName);
    }
}
