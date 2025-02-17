using Application.Attributes;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;

namespace Application.UnitTests.AttributeTests
{
    public class HasUniqueValuesOfPropertiesAttributeTests
    {
        private class TestClass
        {
            public string? Property1 { get; set; }
            public int Property2 { get; set; }
        }

        [Fact]
        public void IsValid_ReturnsSuccess_WhenValuesAreUnique()
        {
            // Arrange
            var attribute = new HasUniqueValuesOfPropertiesAttribute("Property1", "Property2");
            TestClass[] testList =
            [
                new() { Property1 = "A", Property2 = 1 },
                new() { Property1 = "B", Property2 = 2 },
                new() { Property1 = "C", Property2 = 3 }
            ];

            // Act
            var actual = attribute.GetValidationResult(testList, new ValidationContext(testList));

            // Assert
            actual.Should().Be(ValidationResult.Success);
        }

        [Fact]
        public void IsValid_ReturnsError_WhenValuesAreNotUnique()
        {
            // Arrange
            var attribute = new HasUniqueValuesOfPropertiesAttribute("Property1", "Property2");
            TestClass[] testList =
            [
                new() { Property1 = "A", Property2 = 1 },
                new() { Property1 = "B", Property2 = 2 },
                new() { Property1 = "B", Property2 = 3 }
            ];
            ValidationResult expected = new("The values of property 'Property1' must be unique.");

            // Act
            var actual = attribute.GetValidationResult(testList, new ValidationContext(testList));

            // Assert
            actual.Should().NotBeNull();
            actual?.ErrorMessage.Should().Be(expected.ErrorMessage);

        }

        [Fact]
        public void IsValid_ReturnsSuccess_WhenCollectionIsEmpty()
        {
            // Arrange
            var attribute = new HasUniqueValuesOfPropertiesAttribute("Property1", "Property2");
            List<TestClass> testList = [];

            // Act
            var actual = attribute.GetValidationResult(testList, new ValidationContext(testList));

            // Assert
            actual.Should().Be(ValidationResult.Success);
        }

        [Fact]
        public void IsValid_ReturnsSuccess_WhenPropertyIsNull()
        {
            // Arrange
            var attribute = new HasUniqueValuesOfPropertiesAttribute("Property1", "Property2");
            TestClass[] testList =
            [
                new() { Property1 = null, Property2 = 1 },
                new() { Property1 = "B", Property2 = 2 },
                new() { Property1 = "C", Property2 = 3 }
            ];

            // Act
            var actual = attribute.GetValidationResult(testList, new ValidationContext(testList));

            // Assert
            actual.Should().Be(ValidationResult.Success);
        }
    }
}
