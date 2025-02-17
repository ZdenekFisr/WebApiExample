using Application.Features.RailVehicles.Attributes;
using Application.Features.RailVehicles.Model;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;

namespace Application.UnitTests.FeaturesTests.RailVehicles
{
    public class ValidTractionDiagramAttributeTests
    {
        [Fact]
        public void IsValid_ShouldReturnSuccess_WhenTractionDiagramIsValid()
        {
            // Arrange
            VehicleTractionSystemModel tractionSystem = new()
            {
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Speed = 0, PullForce = 300 },
                    new() { Speed = 50, PullForce = 250 },
                    new() { Speed = 100, PullForce = 150 }
                ]
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(tractionSystem, new ValidationContext(tractionSystem));

            // Assert
            result.Should().Be(ValidationResult.Success);
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenSpeedIsOutOfRangeLeft()
        {
            // Arrange
            VehicleTractionSystemModel tractionSystem = new()
            {
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Speed = -1, PullForce = 300 },
                    new() { Speed = 50, PullForce = 250 },
                    new() { Speed = 100, PullForce = 150 }
                ]
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(tractionSystem, new ValidationContext(tractionSystem));

            // Assert
            result?.ErrorMessage.Should().Be("One or more traction diagram points are outside of the speed range.");
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenSpeedIsOutOfRange2()
        {
            // Arrange
            VehicleTractionSystemModel tractionSystem = new()
            {
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Speed = 0, PullForce = 300 },
                    new() { Speed = 50, PullForce = 250 },
                    new() { Speed = 101, PullForce = 150 }
                ]
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(tractionSystem, new ValidationContext(tractionSystem));

            // Assert
            result?.ErrorMessage.Should().Be("One or more traction diagram points are outside of the speed range.");
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenPullForceIsOutOfRange1()
        {
            // Arrange
            VehicleTractionSystemModel tractionSystem = new()
            {
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Speed = 0, PullForce = 300 },
                    new() { Speed = 50, PullForce = 250 },
                    new() { Speed = 100, PullForce = -50 }
                ]
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(tractionSystem, new ValidationContext(tractionSystem));

            // Assert
            result?.ErrorMessage.Should().Be("One or more traction diagram points are outside of the pull force range.");
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenPullForceIsOutOfRange2()
        {
            // Arrange
            VehicleTractionSystemModel tractionSystem = new()
            {
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Speed = 0, PullForce = 350 },
                    new() { Speed = 50, PullForce = 250 },
                    new() { Speed = 100, PullForce = 150 }
                ]
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(tractionSystem, new ValidationContext(tractionSystem));

            // Assert
            result?.ErrorMessage.Should().Be("One or more traction diagram points are outside of the pull force range.");
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenSpeedValuesAreNotUnique()
        {
            // Arrange
            VehicleTractionSystemModel tractionSystem = new()
            {
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Speed = 0, PullForce = 300 },
                    new() { Speed = 100, PullForce = 250 },
                    new() { Speed = 100, PullForce = 150 }
                ]
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(tractionSystem, new ValidationContext(tractionSystem));

            // Assert
            result?.ErrorMessage.Should().Be("The speed of traction diagram points must be unique.");
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenDiagramDoesNotStartAndEndWithCorrectSpeed()
        {
            // Arrange
            VehicleTractionSystemModel tractionSystem = new()
            {
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Speed = 10, PullForce = 300 },
                    new() { Speed = 50, PullForce = 250 },
                    new() { Speed = 90, PullForce = 150 }
                ]
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(tractionSystem, new ValidationContext(tractionSystem));

            // Assert
            result?.ErrorMessage.Should().Be("Traction diagram must start with speed equal to 0 and end with speed equal to the maximum speed of the vehicle.");
        }
    }
}
