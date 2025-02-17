using Application.Features.RailVehicles.Extensions;
using Application.Features.RailVehicles.Model;
using FluentAssertions;

namespace Application.UnitTests.FeaturesTests.RailVehicles
{
    public class TrainExtensionsTests
    {
        [Fact]
        public void GetArrangement_WhenCalled_ReturnsArrangement()
        {
            TrainListModel model = new()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Test",
                TrainVehicles =
                [
                    new() { VehicleId = Guid.NewGuid(), VehicleName = "Vectron", VehicleCount = 1, Position = 0, IsActive = true },
                    new() { VehicleId = Guid.NewGuid(), VehicleName = "TRAXX", VehicleCount = 1, Position = 1, IsActive = false },
                    new() { VehicleId = Guid.NewGuid(), VehicleName = "SGP300", VehicleCount = 3, Position = 2, IsActive = false },
                    new() { VehicleId = Guid.NewGuid(), VehicleName = "742", VehicleCount = 2, Position = 3, IsActive = true }
                ],
                CreatedAt = DateTimeOffset.UtcNow
            };

            string expected = "[Vectron]+TRAXX+3×SGP300+[2×742]";
            string actual = model.GetArrangement();

            actual.Should().Be(expected);
        }
    }
}
