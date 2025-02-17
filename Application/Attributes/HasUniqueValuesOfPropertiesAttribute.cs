using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Application.Attributes
{
    /// <summary>
    /// Attribute to validate that the specified properties of a collection have unique values.
    /// </summary>
    /// <param name="propertyNames">The names of the properties to check for unique values.</param>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class HasUniqueValuesOfPropertiesAttribute(params string[] propertyNames) : ValidationAttribute
    {
        private readonly string[] _propertyNames = propertyNames;

        /// <summary>
        /// Validates whether the specified properties of the collection have unique values.
        /// </summary>
        /// <param name="value">The object to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether validation succeeded or failed.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IEnumerable collection)
            {
                foreach (var propertyName in _propertyNames)
                {
                    var values = collection.Cast<object>()
                                           .Select(x => x.GetType().GetProperty(propertyName)?.GetValue(x))
                                           .Where(val => val != null)
                                           .ToList();

                    var distinctValues = values.Distinct().Count();
                    if (values.Count != distinctValues)
                    {
                        return new ValidationResult($"The values of property '{propertyName}' must be unique.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
