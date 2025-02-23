using Application.Features.RailVehicles.Model;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.DatabaseOperations.HardDelete;
using Infrastructure.DatabaseOperations.Restore;
using Infrastructure.Features.RailVehicles.Repository;

namespace Infrastructure.IntegrationTests.FeaturesTests.RailVehicles
{
    public class TrainDeletedRepositoryTests : TrainIntegrationTestsBase
    {
        private readonly IMapper _mapper;
        private readonly IRestoreOperation _restoreOperation;
        private readonly IHardDeleteOperation _hardDeleteOperation;
        private readonly TrainDeletedRepository _repository;

        public TrainDeletedRepositoryTests(DatabaseFixture databaseFixture)
            : base(databaseFixture)
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
            _restoreOperation = new RestoreOperation();
            _hardDeleteOperation = new HardDeleteOperation();
            _repository = new TrainDeletedRepository(_mapper, _dbContext, _restoreOperation, _hardDeleteOperation);
        }

        [Fact]
        public async Task GetManyAsync_ReturnsTrainListModels()
        {
            (_, Guid[] trainIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            TrainDeletedModel[] expected =
            {
                new() { Id = trainIds[4], Name = "Test Train 6", Description = "Deleted", DeletedAt = new(2024, 12, 6, 22, 51, 54, offset) }
            };

            ICollection<TrainDeletedModel> actual = await _repository.GetManyAsync(user1Id);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task RestoreAsync_ShouldRestoreTrain()
        {
            (_, Guid[] trainIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            await _repository.RestoreAsync(trainIds[4], user1Id);

            Train? restoredEntity = await FindTrainByNameAsync("Test Train 6", user1Id);
            restoredEntity.Should().NotBeNull();
            restoredEntity?.IsDeleted.Should().BeFalse();
            restoredEntity?.DeletedBy.Should().Be(user1Id);
            restoredEntity?.DeletedAt.Should().Be(new(2024, 12, 6, 22, 51, 54, offset));
        }

        [Fact]
        public async Task HardDeleteAsync_ShouldHardDeleteTrain()
        {
            (_, Guid[] trainIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            await _repository.HardDeleteAsync(trainIds[4], user1Id);

            Train? deletedEntity = await FindTrainByNameAsync("Test Train 6", user1Id);
            deletedEntity.Should().BeNull();
        }
    }
}
