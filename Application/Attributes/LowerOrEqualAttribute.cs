using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Application.Attributes
{
    /// <summary>
    /// Attribute to validate that a property is lower than or equal to another specified property.
    /// </summary>
    /// <param name="propertyToCompare">The name of the property to compare with.</param>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class LowerOrEqualAttribute(string propertyToCompare) : ValidationAttribute
    {
        private readonly string _propertyToCompare = propertyToCompare;

        /// <summary>
        /// Validates whether the value of the current property is lower than or equal to the value of the specified property.
        /// </summary>
        /// <param name="value">The value of the current property being validated.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the validation was successful or not.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_propertyToCompare);

            if (property is null)
                return new ValidationResult($"Unknown property: {_propertyToCompare}");

            if (value is IConvertible currentValue &&
            property.GetValue(validationContext.ObjectInstance, null) is IConvertible comparisonValue &&
            currentValue.ToDouble(CultureInfo.InvariantCulture) <= comparisonValue.ToDouble(CultureInfo.InvariantCulture))
                return ValidationResult.Success;

            return new ValidationResult($"The property {validationContext.DisplayName} must be lower than or equal to the property {_propertyToCompare}.");
        }
    }
}
