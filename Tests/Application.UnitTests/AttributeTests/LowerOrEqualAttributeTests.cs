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

        [Theory]
        [InlineData(5, 5)]
        [InlineData(5, 10)]
        public void IsValid_ShouldReturnSuccess(int value1, int value2)
        {
            // Arrange
            var model = new TestModel { Value1 = value1, Value2 = value2 };
            var attribute = new LowerOrEqualAttribute("Value2");
            var validationContext = new ValidationContext(model) { MemberName = "Value1" };

            // Act
            var result = attribute.GetValidationResult(model.Value1, validationContext);

            // Assert
            result.Should().Be(ValidationResult.Success);
        }

        [Theory]
        [InlineData(15, 10, "Value2", "The property Value1 must be lower than or equal to the property Value2.")]
        [InlineData(5, 10, "Value3", "Unknown property: Value3")]
        public void IsValid_ShouldReturnValidationError(int value1, int value2, string propertyToCompare, string expectedErrorMessage)
        {
            // Arrange
            var model = new TestModel { Value1 = value1, Value2 = value2 };
            var attribute = new LowerOrEqualAttribute(propertyToCompare);
            var validationContext = new ValidationContext(model) { MemberName = "Value1" };

            // Act
            var result = attribute.GetValidationResult(model.Value1, validationContext);

            // Assert
            result.Should().NotBe(ValidationResult.Success);
            result?.ErrorMessage.Should().Be(expectedErrorMessage);
        }
    }
}
