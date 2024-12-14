using Application.Features.RailVehicles.Attributes;
using Application.Features.RailVehicles.Model;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;

namespace Application.UnitTests.FeaturesTests.RailVehicles
{
    public class ValidTractionDiagramAttributeTests
    {
        private class TestVehicleDrivingDto : RailVehicleDrivingModelBase
        {
        }

        [Fact]
        public void IsValid_ShouldReturnSuccess_WhenTractionDiagramIsValid()
        {
            // Arrange
            List<TractionDiagramPointModel> tractionDiagram =
            [
                new() { Speed = 0, PullForce = 300 },
                new() { Speed = 50, PullForce = 250 },
                new() { Speed = 100, PullForce = 150 }
            ];
            TestVehicleDrivingDto vehicle = new()
            {
                Name = "Test",
                Description = string.Empty,
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = tractionDiagram
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(vehicle, new ValidationContext(vehicle));

            // Assert
            result.Should().Be(ValidationResult.Success);
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenSpeedIsOutOfRange1()
        {
            // Arrange
            List<TractionDiagramPointModel> tractionDiagram =
            [
                new() { Speed = -1, PullForce = 300 },
                new() { Speed = 50, PullForce = 250 },
                new() { Speed = 100, PullForce = 150 }
            ];
            TestVehicleDrivingDto vehicle = new()
            {
                Name = "Test",
                Description = string.Empty,
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = tractionDiagram
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(vehicle, new ValidationContext(vehicle));

            // Assert
            result?.ErrorMessage.Should().Be("One or more traction diagram points are outside of the speed range.");
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenSpeedIsOutOfRange2()
        {
            // Arrange
            List<TractionDiagramPointModel> tractionDiagram =
            [
                new() { Speed = 0, PullForce = 300 },
                new() { Speed = 50, PullForce = 250 },
                new() { Speed = 101, PullForce = 150 }
            ];
            TestVehicleDrivingDto vehicle = new()
            {
                Name = "Test",
                Description = string.Empty,
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = tractionDiagram
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(vehicle, new ValidationContext(vehicle));

            // Assert
            result?.ErrorMessage.Should().Be("One or more traction diagram points are outside of the speed range.");
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenPullForceIsOutOfRange1()
        {
            // Arrange
            List<TractionDiagramPointModel> tractionDiagram =
            [
                new() { Speed = 0, PullForce = 300 },
                new() { Speed = 50, PullForce = 250 },
                new() { Speed = 100, PullForce = -50 }
            ];
            TestVehicleDrivingDto vehicle = new()
            {
                Name = "Test",
                Description = string.Empty,
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = tractionDiagram
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(vehicle, new ValidationContext(vehicle));

            // Assert
            result?.ErrorMessage.Should().Be("One or more traction diagram points are outside of the pull force range.");
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenPullForceIsOutOfRange2()
        {
            // Arrange
            List<TractionDiagramPointModel> tractionDiagram =
            [
                new() { Speed = 0, PullForce = 350 },
                new() { Speed = 50, PullForce = 250 },
                new() { Speed = 100, PullForce = 150 }
            ];
            TestVehicleDrivingDto vehicle = new()
            {
                Name = "Test",
                Description = string.Empty,
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = tractionDiagram
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(vehicle, new ValidationContext(vehicle));

            // Assert
            result?.ErrorMessage.Should().Be("One or more traction diagram points are outside of the pull force range.");
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenSpeedValuesAreNotUnique()
        {
            // Arrange
            List<TractionDiagramPointModel> tractionDiagram =
            [
                new() { Speed = 0, PullForce = 300 },
                new() { Speed = 100, PullForce = 250 },
                new() { Speed = 100, PullForce = 150 }
            ];
            TestVehicleDrivingDto vehicle = new()
            {
                Name = "Test",
                Description = string.Empty,
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = tractionDiagram
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(vehicle, new ValidationContext(vehicle));

            // Assert
            result?.ErrorMessage.Should().Be("The speed of traction diagram points must be unique.");
        }

        [Fact]
        public void IsValid_ShouldReturnError_WhenDiagramDoesNotStartAndEndWithCorrectSpeed()
        {
            // Arrange
            List<TractionDiagramPointModel> tractionDiagram =
            [
                new() { Speed = 10, PullForce = 300 },
                new() { Speed = 50, PullForce = 250 },
                new() { Speed = 90, PullForce = 150 }
            ];
            TestVehicleDrivingDto vehicle = new()
            {
                Name = "Test",
                Description = string.Empty,
                MaxSpeed = 100,
                MaxPullForce = 300,
                TractionDiagram = tractionDiagram
            };
            ValidTractionDiagramAttribute attribute = new();

            // Act
            ValidationResult? result = attribute.GetValidationResult(vehicle, new ValidationContext(vehicle));

            // Assert
            result?.ErrorMessage.Should().Be("Traction diagram must start with speed equal to 0 and end with speed equal to the maximum speed of the vehicle.");
        }
    }
}
