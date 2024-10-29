namespace Application.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// If the key is not present in the dictionary, a new value with this key is added. Otherwise, an old value with this key gets replaced by the new value.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key">Key to search in the dictionary.</param>
        /// <param name="newValue">Value to either add or use as a replacement.</param>
        public static void AddOrReplaceValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue newValue)
            where TKey : notnull
        {
            if (!dictionary.TryAdd(key, newValue))
                dictionary[key] = newValue;
        }
    }
}
