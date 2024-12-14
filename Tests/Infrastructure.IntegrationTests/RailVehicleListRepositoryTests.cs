using Application.Features.RailVehicles.ListModel;
using Application.Features.RailVehicles.Repository;
using Application.Helpers;
using AutoMapper;
using FluentAssertions;
using Infrastructure.Features.RailVehicles.Repository;

namespace Infrastructure.IntegrationTests
{
    [Collection("Database")]
    public class RailVehicleListRepositoryTests : IAsyncLifetime
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Func<Task> _resetDatabase;

        private readonly IMapper _mapper;
        private readonly IRailVehicleListRepository<RailVehicleListModel> _repository;

        private readonly EntityProvider _vehiclesProvider = new();

        private readonly TimeSpan offset = new(0);

        public RailVehicleListRepositoryTests(DatabaseFixture databaseFixture)
        {
            _resetDatabase = databaseFixture.ResetDatabase;

            _dbContext = databaseFixture.Context;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
            _repository = new RailVehicleListRepository(_mapper, _dbContext);
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync() => await _resetDatabase();

        [Fact]
        public async Task GetManyAsync_ReturnsRailVehicleListModels()
        {
            // Arrange
            Guid[] vehicleIds = GuidHelpers.GenerateRandomGuids(8).ToArray();
            string userId1 = Guid.NewGuid().ToString();
            string userId2 = Guid.NewGuid().ToString();

            RailVehicleListModel[] expected =
            [
                new() { Id = vehicleIds[0], Name = "Test Vehicle 1", Description = "Dependent", MaxSpeed = 200, Performance = 6400, MaxPullForce = 300, CreatedAt = new(2024, 12, 6, 16, 32, 51, offset) },
                new() { Id = vehicleIds[1], Name = "Test Vehicle 2", Description = "Pulled", MaxSpeed = 200, Performance = 0, MaxPullForce = 0, CreatedAt = new(2024, 12, 6, 16, 35, 6, offset), UpdatedAt = new(2024, 12, 6, 16, 35, 55, offset) },
                new() { Id = vehicleIds[2], Name = "Test Vehicle 3", Description = "Independent", MaxSpeed = 100, Performance = 3200, MaxPullForce = 300, CreatedAt = new(2024, 12, 6, 16, 39, 32, offset), UpdatedAt = new(2024, 12, 6, 16, 40, 47, offset) },
                new() { Id = vehicleIds[3], Name = "Test Vehicle 4", Description = "Hybrid", MaxSpeed = 100, Performance = 3200, MaxPullForce = 300, CreatedAt = new(2024, 12, 6, 16, 45, 2, offset) }
            ];

            await _dbContext.Users.AddRangeAsync([new() { Id = userId1 }, new() { Id = userId2 }]);
            await _dbContext.AddRangeAsync(_vehiclesProvider.GetTestVehicles(vehicleIds, userId1, userId2));
            await _dbContext.SaveChangesAsync();

            // Act
            ICollection<RailVehicleListModel> actual = await _repository.GetManyAsync(userId1);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
