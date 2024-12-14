using Application.Extensions;
using FluentAssertions;

namespace Application.UnitTests.ExtensionsTests
{
    public class CollectionExtensionsTests
    {
        private class TestObject
        {
            public int Id { get; set; }
            public required string Name { get; set; }
        }

        [Fact]
        public void HasUniqueValuesOfProperties_ShouldReturnTrue_WhenAllValuesAreUnique()
        {
            // Arrange
            List<TestObject> collection =
            [
                new() { Id = 1, Name = "A" },
                new() { Id = 2, Name = "B" },
                new() { Id = 3, Name = "C" }
            ];

            // Act
            bool result = collection.HasUniqueValuesOfProperties(x => x.Id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void HasUniqueValuesOfProperties_ShouldReturnFalse_WhenValuesAreNotUnique()
        {
            // Arrange
            List<TestObject> collection =
            [
                new() { Id = 1, Name = "A" },
                new() { Id = 2, Name = "B" },
                new() { Id = 1, Name = "C" }
            ];

            // Act
            bool result = collection.HasUniqueValuesOfProperties(x => x.Id);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void HasUniqueValuesOfProperties_ShouldReturnTrue_WhenCollectionIsEmpty()
        {
            // Arrange
            List<TestObject> collection = [];

            // Act
            var result = collection.HasUniqueValuesOfProperties(x => x.Id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void HasUniqueValuesOfProperties_ShouldReturnTrue_WhenSingleElementInCollection()
        {
            // Arrange
            List<TestObject> collection =
            [
                new() { Id = 1, Name = "A" }
            ];

            // Act
            var result = collection.HasUniqueValuesOfProperties(x => x.Id);

            // Assert
            result.Should().BeTrue();
        }
    }
}
