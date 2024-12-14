using Application.Extensions;
using Application.Features.RailVehicles.Model;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.RailVehicles.Attributes
{
    /// <summary>
    /// Attribute to validate the traction diagram of a vehicle.
    /// Ensures that the traction diagram points are within the valid speed and pull force range,
    /// have unique speed values, and start and end with the correct speed values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ValidTractionDiagramAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the traction diagram of a vehicle.
        /// Ensures that the traction diagram points are within the valid speed and pull force range,
        /// have unique speed values, and start and end with the correct speed values.
        /// </summary>
        /// <param name="value">The object to validate, expected to be of type <see cref="RailVehicleDrivingModelBase"/>.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the traction diagram is valid or not.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the attribute is applied to a type other than <see cref="RailVehicleDrivingModelBase"/>.</exception>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not RailVehicleDrivingModelBase model)
            {
                throw new InvalidOperationException($"{nameof(ValidTractionDiagramAttribute)} can only be applied to classes derived from '{nameof(RailVehicleDrivingModelBase)}'.");
            }

            model.TractionDiagram = [.. model.TractionDiagram.OrderBy(tdp => tdp.Speed)];

            foreach (var tdp in model.TractionDiagram)
            {
                if (tdp.Speed < 0 || tdp.Speed > model.MaxSpeed)
                    return new ValidationResult("One or more traction diagram points are outside of the speed range.");

                if (tdp.PullForce < 0 || tdp.PullForce > model.MaxPullForce)
                    return new ValidationResult("One or more traction diagram points are outside of the pull force range.");
            }

            if (!model.TractionDiagram.HasUniqueValuesOfProperties(tdp => tdp.Speed))
                return new ValidationResult("The speed of traction diagram points must be unique.");

            if (model.TractionDiagram.Count > 0 && (!model.TractionDiagram.Any(tdp => tdp.Speed == 0) || !model.TractionDiagram.Any(tdp => tdp.Speed == model.MaxSpeed)))
                return new ValidationResult("Traction diagram must start with speed equal to 0 and end with speed equal to the maximum speed of the vehicle.");

            return ValidationResult.Success;
        }
    }
}
