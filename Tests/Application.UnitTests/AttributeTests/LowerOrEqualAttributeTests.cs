using Application.Attributes;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;

namespace Application.UnitTests.AttributeTests
{
    public class LowerOrEqualAttributeTests
    {
        private class TestModel
        {
            public int Value1 { get; set; }
            public int Value2 { get; set; }
        }

        [Fact]
        public void IsValid_WhenValueIsEqual_ShouldReturnSuccess()
        {
            // Arrange
            var model = new TestModel { Value1 = 5, Value2 = 5 };
            var attribute = new LowerOrEqualAttribute("Value2");
            var validationContext = new ValidationContext(model) { MemberName = "Value1" };

            // Act
            var result = attribute.GetValidationResult(model.Value1, validationContext);

            // Assert
            result.Should().Be(ValidationResult.Success);
        }

        [Fact]
        public void IsValid_WhenValueIsLower_ShouldReturnSuccess()
        {
            // Arrange
            var model = new TestModel { Value1 = 5, Value2 = 10 };
            var attribute = new LowerOrEqualAttribute("Value2");
            var validationContext = new ValidationContext(model) { MemberName = "Value1" };

            // Act
            var result = attribute.GetValidationResult(model.Value1, validationContext);

            // Assert
            result.Should().Be(ValidationResult.Success);
        }

        [Fact]
        public void IsValid_WhenValueIsGreater_ShouldReturnValidationError()
        {
            // Arrange
            var model = new TestModel { Value1 = 15, Value2 = 10 };
            var attribute = new LowerOrEqualAttribute("Value2");
            var validationContext = new ValidationContext(model) { MemberName = "Value1" };

            // Act
            var result = attribute.GetValidationResult(model.Value1, validationContext);

            // Assert
            result.Should().NotBe(ValidationResult.Success);
            result?.ErrorMessage.Should().Be("The property Value1 must be lower than or equal to the property Value2.");
        }

        [Fact]
        public void IsValid_WhenPropertyToCompareIsUnknown_ShouldReturnValidationError()
        {
            // Arrange
            var model = new TestModel { Value1 = 5, Value2 = 10 };
            var attribute = new LowerOrEqualAttribute("Value3");
            var validationContext = new ValidationContext(model) { MemberName = "Value1" };

            // Act
            var result = attribute.GetValidationResult(model.Value1, validationContext);

            // Assert
            result.Should().NotBe(ValidationResult.Success);
            result?.ErrorMessage.Should().Be("Unknown property: Value3");
        }
    }
}
