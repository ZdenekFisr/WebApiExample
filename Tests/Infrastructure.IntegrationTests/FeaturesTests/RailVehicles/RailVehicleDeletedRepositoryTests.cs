using Application.Features.RailVehicles.Model;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.DatabaseOperations.HardDelete;
using Infrastructure.DatabaseOperations.Restore;
using Infrastructure.Features.RailVehicles.Repository;

namespace Infrastructure.IntegrationTests.FeaturesTests.RailVehicles
{
    public class RailVehicleDeletedRepositoryTests : RailVehicleIntegrationTestsBase
    {
        private readonly IMapper _mapper;
        private readonly IRestoreOperation _restoreOperation;
        private readonly IHardDeleteOperation _hardDeleteOperation;
        private readonly RailVehicleDeletedRepository _repository;

        public RailVehicleDeletedRepositoryTests(DatabaseFixture databaseFixture)
            : base(databaseFixture)
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
            _restoreOperation = new RestoreOperation();
            _hardDeleteOperation = new HardDeleteOperation();
            _repository = new RailVehicleDeletedRepository(_mapper, _dbContext, _restoreOperation, _hardDeleteOperation);
        }

        [Fact]
        public async Task GetManyAsync_ReturnsRailVehicleListModels()
        {
            (Guid[] vehicleIds, _, string user1Id, _) = await AddTestEntitiesToDbAsync();

            RailVehicleDeletedModel[] expected =
            [
                new() { Id = vehicleIds[4], Name = "Test Vehicle 5", Description = "Dependent deleted", DeletedAt = new(2024, 12, 6, 16, 58, 26, offset) },
                new() { Id = vehicleIds[5], Name = "Test Vehicle 6", Description = "Pulled deleted", DeletedAt = new(2024, 12, 6, 16, 58, 55, offset) }
            ];

            ICollection<RailVehicleDeletedModel> actual = await _repository.GetManyAsync(user1Id);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task RestoreAsync_ShouldRestoreVehicle()
        {
            (Guid[] vehicleIds, _, string user1Id, _) = await AddTestEntitiesToDbAsync();

            await _repository.RestoreAsync(vehicleIds[4], user1Id);

            RailVehicle? restoredEntity = await FindVehicleByNameAsync("Test Vehicle 5", user1Id);
            restoredEntity.Should().NotBeNull();
            restoredEntity?.IsDeleted.Should().BeFalse();
            restoredEntity?.DeletedBy.Should().Be(user1Id);
            restoredEntity?.DeletedAt.Should().Be(new(2024, 12, 6, 16, 58, 26, offset));
        }

        [Fact]
        public async Task HardDeleteAsync_ShouldHardDeleteVehicle()
        {
            (Guid[] vehicleIds, _, string user1Id, _) = await AddTestEntitiesToDbAsync();

            await _repository.HardDeleteAsync(vehicleIds[4], user1Id);

            RailVehicle? deletedEntity = await FindVehicleByNameAsync("Test Vehicle 5", user1Id);
            deletedEntity.Should().BeNull();
        }
    }
}
