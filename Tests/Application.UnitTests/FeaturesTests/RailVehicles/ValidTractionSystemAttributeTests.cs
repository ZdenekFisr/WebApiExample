using Application.Features.RailVehicles.Attributes;
using Application.Features.RailVehicles.Model;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;

namespace Application.UnitTests.FeaturesTests.RailVehicles
{
    public class ValidTractionSystemAttributeTests
    {
        [Fact]
        public void IsValid_ShouldReturnSuccess_WhenTractionSystemIsValid()
        {
            // Arrange
            RailVehicleDrivingModel vehicle = new()
            {
                Name = "Test",
                Wheelsets = 8,
                MaxSpeed = 160,
                TractionSystems = [
                    new() { ElectrificationTypeId = Guid.NewGuid(), DrivingWheelsets = 8, MaxSpeed = 160, TractionDiagram = [] },
                    new() { ElectrificationTypeId = null, DrivingWheelsets = 2, MaxSpeed = 100, TractionDiagram = [] }
                ]
            };
            ValidTractionSystemAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(vehicle, new ValidationContext(vehicle));

            // Assert
            result.Should().Be(ValidationResult.Success);
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenIndependentTractionSystemsCountIsTooLarge()
        {
            // Arrange
            RailVehicleDrivingModel vehicle = new()
            {
                Name = "Test",
                Wheelsets = 8,
                MaxSpeed = 160,
                TractionSystems = [
                    new() { ElectrificationTypeId = Guid.NewGuid(), DrivingWheelsets = 8, MaxSpeed = 160, TractionDiagram = [] },
                    new() { ElectrificationTypeId = null, DrivingWheelsets = 2, MaxSpeed = 100, TractionDiagram = [] },
                    new() { ElectrificationTypeId = null, DrivingWheelsets = 2, MaxSpeed = 100, TractionDiagram = [] }
                ]
            };
            ValidTractionSystemAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(vehicle, new ValidationContext(vehicle));

            // Assert
            result?.ErrorMessage.Should().Be("Only one traction system can be independent.");
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenDrivingWheelsetsCountIsTooLarge()
        {
            // Arrange
            RailVehicleDrivingModel vehicle = new()
            {
                Name = "Test",
                Wheelsets = 8,
                MaxSpeed = 160,
                TractionSystems = [
                    new() { ElectrificationTypeId = Guid.NewGuid(), DrivingWheelsets = 12, MaxSpeed = 160, TractionDiagram = [] },
                    new() { ElectrificationTypeId = null, DrivingWheelsets = 2, MaxSpeed = 100, TractionDiagram = [] }
                ]
            };
            ValidTractionSystemAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(vehicle, new ValidationContext(vehicle));

            // Assert
            result?.ErrorMessage.Should().Be("The number of driving wheelsets must be equal to or lower than the number of wheelsets in the vehicle.");
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenMaxSpeedIsTooLarge()
        {
            // Arrange
            RailVehicleDrivingModel vehicle = new()
            {
                Name = "Test",
                Wheelsets = 8,
                MaxSpeed = 160,
                TractionSystems = [
                    new() { ElectrificationTypeId = Guid.NewGuid(), DrivingWheelsets = 8, MaxSpeed = 200, TractionDiagram = [] },
                    new() { ElectrificationTypeId = null, DrivingWheelsets = 2, MaxSpeed = 100, TractionDiagram = [] }
                ]
            };
            ValidTractionSystemAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(vehicle, new ValidationContext(vehicle));

            // Assert
            result?.ErrorMessage.Should().Be("The maximum speed of the traction system must be equal to or lower than the maximum speed of the vehicle.");
        }
    }
}
