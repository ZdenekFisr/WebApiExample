namespace Application.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines whether the specified collection has unique values for a given property.
        /// </summary>
        /// <typeparam name="TObject">The type of the objects in the collection.</typeparam>
        /// <typeparam name="TProperty">The type of the property to check for uniqueness.</typeparam>
        /// <param name="collection">The collection of objects to check.</param>
        /// <param name="property">A function to extract the property value from an object.</param>
        /// <returns><c>true</c> if the collection has unique values for the specified property; otherwise, <c>false</c>.</returns>
        public static bool HasUniqueValuesOfProperties<TObject, TProperty>(this ICollection<TObject> collection, Func<TObject, TProperty> property)
            => collection.Select(property).Distinct().Count() == collection.Count;
    }
}
