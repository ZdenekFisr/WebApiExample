using Application.Features.RailVehicles.ListModel;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.DatabaseOperations.Restore;
using Infrastructure.Features.RailVehicles.Repository;

namespace Infrastructure.IntegrationTests
{
    public class RailVehicleDeletedRepositoryTests : RailVehicleIntegrationTestsBase
    {
        private readonly IMapper _mapper;
        private readonly IRestoreOperation _restoreOperation;
        private readonly RailVehicleDeletedRepository _repository;

        public RailVehicleDeletedRepositoryTests(DatabaseFixture databaseFixture)
            : base(databaseFixture)
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
            _restoreOperation = new RestoreOperation<RailVehicle>();
            _repository = new RailVehicleDeletedRepository(_mapper, _dbContext, _restoreOperation);
        }

        [Fact]
        public async Task GetManyAsync_ReturnsRailVehicleListModels()
        {
            (Guid[] vehicleIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            RailVehicleListModel[] expected =
            [
                new() { Id = vehicleIds[4], Name = "Test Vehicle 5", Description = "Dependent deleted", MaxSpeed = 200, Performance = 6400, MaxPullForce = 300, CreatedAt = new(2024, 12, 6, 16, 52, 13, offset) },
                new() { Id = vehicleIds[5], Name = "Test Vehicle 6", Description = "Pulled deleted", MaxSpeed = 200, Performance = 0, MaxPullForce = 0, CreatedAt = new(2024, 12, 6, 16, 55, 10, offset) }
            ];

            ICollection<RailVehicleListModel> actual = await _repository.GetManyAsync(user1Id);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task RestoreAsync_ShouldRestoreVehicle()
        {
            (Guid[] vehicleIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            await _repository.RestoreAsync(vehicleIds[4], user1Id);

            RailVehicle? restoredEntity = await FindVehicleByNameAsync("Test Vehicle 5", user1Id);
            restoredEntity.Should().NotBeNull();
            restoredEntity?.IsDeleted.Should().BeFalse();
            restoredEntity?.DeletedBy.Should().Be(user1Id);
            restoredEntity?.DeletedAt.Should().Be(new(2024, 12, 6, 16, 58, 26, offset));
        }
    }
}
