using Application.Features.RailVehicles.ListModel;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.DatabaseOperations.SoftDelete;
using Infrastructure.Features.RailVehicles.Repository;
using Infrastructure.Services;

namespace Infrastructure.IntegrationTests
{
    [Collection("Database")]
    public class RailVehicleListRepositoryTests : RailVehicleIntegrationTestsBase
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUtcTimeProvider _timeProvider = new CurrentUtcTimeProvider();
        private readonly ISoftDeleteOperation _softDeleteOperation;
        private readonly RailVehicleListRepository _repository;

        public RailVehicleListRepositoryTests(DatabaseFixture databaseFixture)
            : base(databaseFixture)
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
            _timeProvider = new CurrentUtcTimeProvider();
            _softDeleteOperation = new SoftDeleteOperation<RailVehicle>(_timeProvider);
            _repository = new RailVehicleListRepository(_mapper, _dbContext, _softDeleteOperation);
        }

        [Fact]
        public async Task GetManyAsync_ReturnsRailVehicleListModels()
        {
            (Guid[] vehicleIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            RailVehicleListModel[] expected =
            [
                new() { Id = vehicleIds[0], Name = "Test Vehicle 1", Description = "Dependent", MaxSpeed = 200, Performance = 6400, MaxPullForce = 300, CreatedAt = new(2024, 12, 6, 16, 32, 51, offset) },
                new() { Id = vehicleIds[1], Name = "Test Vehicle 2", Description = "Pulled", MaxSpeed = 200, Performance = 0, MaxPullForce = 0, CreatedAt = new(2024, 12, 6, 16, 35, 6, offset), UpdatedAt = new(2024, 12, 6, 16, 35, 55, offset) },
                new() { Id = vehicleIds[2], Name = "Test Vehicle 3", Description = "Independent", MaxSpeed = 100, Performance = 3200, MaxPullForce = 300, CreatedAt = new(2024, 12, 6, 16, 39, 32, offset), UpdatedAt = new(2024, 12, 6, 16, 40, 47, offset) },
                new() { Id = vehicleIds[3], Name = "Test Vehicle 4", Description = "Hybrid", MaxSpeed = 100, Performance = 3200, MaxPullForce = 300, CreatedAt = new(2024, 12, 6, 16, 45, 2, offset) }
            ];

            ICollection<RailVehicleListModel> actual = await _repository.GetManyAsync(user1Id);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task SoftDeleteAsync_ShouldSoftDeleteVehicle()
        {
            (Guid[] vehicleIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            await _repository.SoftDeleteAsync(vehicleIds[2], user1Id);

            RailVehicle? deletedEntity = await FindVehicleByNameAsync("Test Vehicle 3", user1Id);
            deletedEntity.Should().NotBeNull();
            deletedEntity?.IsDeleted.Should().BeTrue();
            deletedEntity?.DeletedBy.Should().Be(user1Id);
            deletedEntity?.DeletedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, timeDelta);
        }
    }
}
