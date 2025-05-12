using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.DatabaseOperations.SoftDelete;
using Infrastructure.Features.RailVehicles.Repository;
using Infrastructure.Services;

namespace Infrastructure.IntegrationTests.FeaturesTests.RailVehicles
{
    public class TrainListRepositoryTests : TrainIntegrationTestsBase
    {
        private readonly IRailVehicleNameRepository _vehicleNameRepository;
        private readonly ICurrentUtcTimeProvider _timeProvider;
        private readonly ISoftDeleteOperation _softDeleteOperation;
        private readonly TrainListRepository _repository;

        public TrainListRepositoryTests(DatabaseFixture databaseFixture)
            : base(databaseFixture)
        {
            _vehicleNameRepository = new RailVehicleNameRepository(_dbContext);
            _timeProvider = new CurrentUtcTimeProvider();
            _softDeleteOperation = new SoftDeleteOperation(_timeProvider);
            _repository = new TrainListRepository(_dbContext, _vehicleNameRepository, _softDeleteOperation);
        }

        [Fact]
        public async Task GetAllAsync_WithUserId_ReturnsAllTrains()
        {
            // Arrange
            (Guid[] vehicleIds, Guid[] trainIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            TrainListModel[] expected =
            [
                new()
                {
                    Id = trainIds[0],
                    Name = "Test Train 1",
                    Description = "Mixed",
                    TrainVehicles = [
                        new() { VehicleId = vehicleIds[0], VehicleName = "Test Vehicle 1", VehicleCount = 1, Position = 0, IsActive = true },
                        new() { VehicleId = vehicleIds[1], VehicleName = "Test Vehicle 2", VehicleCount = 3, Position = 1 },
                        new() { VehicleId = vehicleIds[2], VehicleName = "Test Vehicle 3", VehicleCount = 2, Position = 2 },
                        new() { VehicleId = vehicleIds[3], VehicleName = "Test Vehicle 4", VehicleCount = 2, Position = 3, IsActive = true }
                    ],
                    Arrangement = "[Test Vehicle 1]+3×Test Vehicle 2+2×Test Vehicle 3+[2×Test Vehicle 4]",
                    CreatedAt = new(2024, 12, 6, 19, 34, 12, offset),
                },
                new()
                {
                    Id = trainIds[1],
                    Name = "Test Train 2",
                    Description = "With deleted vehicle",
                    TrainVehicles = [
                        new() { VehicleId = vehicleIds[0], VehicleName = "Test Vehicle 1", VehicleCount = 1, Position = 0, IsActive = true }
                    ],
                    Arrangement = "[Test Vehicle 1]",
                    CreatedAt = new(2024, 12, 6, 19, 37, 33, offset),
                    UpdatedAt = new(2024, 12, 6, 22, 50, 4, offset)
                },
                new()
                {
                    Id = trainIds[3],
                    Name = "Test Train 5",
                    Description = "Empty",
                    TrainVehicles = [],
                    Arrangement = string.Empty,
                    CreatedAt = new(2024, 12, 6, 20, 0, 47, offset)
                }
            ];

            // Act
            ICollection<TrainListModel> actual = await _repository.GetManyAsync(user1Id);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task SoftDeleteAsync_ShouldSoftDeleteTrain()
        {
            (_, Guid[] trainIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            await _repository.SoftDeleteAsync(trainIds[0], user1Id);

            Train? deletedEntity = await FindTrainByNameAsync("Test Train 1", user1Id);
            deletedEntity.Should().NotBeNull();
            deletedEntity?.IsDeleted.Should().BeTrue();
            deletedEntity?.DeletedBy.Should().Be(user1Id);
            deletedEntity?.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, timeDelta);
        }
    }
}
