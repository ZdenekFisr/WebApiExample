using Application.Features.RailVehicles.Model;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.RailVehicles.Attributes
{
    /// <summary>
    /// Validates the traction system of a vehicle.
    /// Ensures that the parameters of the traction system align with the parameters of the vehicle.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class ValidTractionSystemAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the traction system of a vehicle.
        /// Ensures that the parameters of the traction system align with the parameters of the vehicle.
        /// </summary>
        /// <param name="value">The object to validate, expected to be of type <see cref="RailVehicleDrivingModel"/>.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the traction system is valid or not.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the attribute is applied to a type other than <see cref="RailVehicleDrivingModel"/>.</exception>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not RailVehicleDrivingModel vehicle)
            {
                throw new InvalidOperationException($"{nameof(ValidTractionSystemAttribute)} can only be applied to classes implementing '{nameof(RailVehicleDrivingModel)}'.");
            }

            int independentCount = vehicle.TractionSystems.Count(ts => !ts.ElectrificationTypeId.HasValue);
            if (independentCount > 1)
                return new ValidationResult("Only one traction system can be independent.");

            foreach (VehicleTractionSystemModel ts in vehicle.TractionSystems)
            {
                if (ts.DrivingWheelsets > vehicle.Wheelsets)
                    return new ValidationResult("The number of driving wheelsets must be equal to or lower than the number of wheelsets in the vehicle.");

                if (ts.MaxSpeed > vehicle.MaxSpeed)
                    return new ValidationResult("The maximum speed of the traction system must be equal to or lower than the maximum speed of the vehicle.");
            }

            return ValidationResult.Success;
        }
    }
}
