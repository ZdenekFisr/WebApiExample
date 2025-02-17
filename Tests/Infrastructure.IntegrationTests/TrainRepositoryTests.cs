using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.DatabaseOperations.Insert;
using Infrastructure.DatabaseOperations.SoftDelete;
using Infrastructure.DatabaseOperations.Update;
using Infrastructure.Features.RailVehicles.Repository;
using Infrastructure.Services;

namespace Infrastructure.IntegrationTests
{
    public class TrainRepositoryTests : TrainIntegrationTestsBase
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider;
        private readonly IRailVehicleNameRepository _vehicleNameRepository;
        private readonly IInsertOperation _insertOperation;
        private readonly IUpdateOperation _updateOperation;
        private readonly TrainRepository _repository;

        public TrainRepositoryTests(DatabaseFixture databaseFixture)
            : base(databaseFixture)
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
            _currentUtcTimeProvider = new CurrentUtcTimeProvider();
            _vehicleNameRepository = new RailVehicleNameRepository(_dbContext);
            _insertOperation = new InsertOperation(_mapper, _currentUtcTimeProvider);
            _updateOperation = new UpdateOperation(_mapper, _currentUtcTimeProvider);
            _repository = new TrainRepository(_mapper, _dbContext, _vehicleNameRepository, _insertOperation, _updateOperation);
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnTrain()
        {
            (Guid[] vehicleIds, Guid[] trainIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            TrainOutputModel expected = new()
            {
                Name = "Test Train 1",
                Description = "Mixed",
                MaxPullForce = 300,
                TrainVehicles =
                [
                    new() { VehicleId = vehicleIds[0], VehicleName = "Test Vehicle 1", VehicleCount = 1, Position = 0, IsActive = true },
                    new() { VehicleId = vehicleIds[1], VehicleName = "Test Vehicle 2", VehicleCount = 3, Position = 1 },
                    new() { VehicleId = vehicleIds[2], VehicleName = "Test Vehicle 3", VehicleCount = 2, Position = 2 },
                    new() { VehicleId = vehicleIds[3], VehicleName = "Test Vehicle 4", VehicleCount = 2, Position = 3, IsActive = true }
                ],
            };

            TrainOutputModel? actual = await _repository.GetOneAsync(trainIds[0], user1Id);

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnTrainWithoutDeletedVehicles()
        {
            (Guid[] vehicleIds, Guid[] trainIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            TrainOutputModel expected = new()
            {
                Name = "Test Train 2",
                Description = "With deleted vehicle",
                MaxPullForce = 200,
                TrainVehicles =
                [
                    new() { VehicleId = vehicleIds[0], VehicleName = "Test Vehicle 1", VehicleCount = 1, Position = 0, IsActive = true }
                ],
            };

            TrainOutputModel? actual = await _repository.GetOneAsync(trainIds[1], user1Id);

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnNull_WhenTrainDoesNotExist()
        {
            (_, _, string user1Id, _) = await AddTestEntitiesToDbAsync();

            TrainOutputModel? actual = await _repository.GetOneAsync(Guid.NewGuid(), user1Id);

            actual.Should().BeNull();
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnNull_WhenTrainBelongsToAnotherUser()
        {
            (_, Guid[] trainIds, _, string user2Id) = await AddTestEntitiesToDbAsync();

            TrainOutputModel? actual = await _repository.GetOneAsync(trainIds[0], user2Id);

            actual.Should().BeNull();
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnNull_WhenTrainIsDeleted()
        {
            (_, Guid[] trainIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            TrainOutputModel? actual = await _repository.GetOneAsync(trainIds[4], user1Id);

            actual.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_ShouldInsertTrain()
        {
            (Guid[] vehicleIds, _, string user1Id, _) = await AddTestEntitiesToDbAsync();
            string trainName = "Test Train - create";

            TrainOutputModel expected = new()
            {
                Name = trainName,
                Description = "Test create",
                MaxPullForce = 200,
                TrainVehicles =
                [
                    new() { VehicleId = vehicleIds[0], VehicleName = "Test Vehicle 1", VehicleCount = 1, Position = 0, IsActive = true },
                    new() { VehicleId = vehicleIds[1], VehicleName = "Test Vehicle 2", VehicleCount = 3, Position = 1 },
                    new() { VehicleId = vehicleIds[2], VehicleName = "Test Vehicle 3", VehicleCount = 2, Position = 2 },
                    new() { VehicleId = vehicleIds[3], VehicleName = "Test Vehicle 4", VehicleCount = 2, Position = 3 }
                ]
            };

            TrainInputModel newModel = new()
            {
                Name = trainName,
                Description = "Test create",
                MaxPullForce = 200,
                TrainVehicles =
                [
                    new() { VehicleId = vehicleIds[0], VehicleCount = 1, Position = 0, IsActive = true },
                    new() { VehicleId = vehicleIds[1], VehicleCount = 3, Position = 1 },
                    new() { VehicleId = vehicleIds[2], VehicleCount = 2, Position = 2 },
                    new() { VehicleId = vehicleIds[3], VehicleCount = 2, Position = 3 }
                ]
            };
            await _repository.CreateAsync(newModel, user1Id);

            TrainListRepository trainListRepository = new(_mapper, _dbContext, _vehicleNameRepository, new SoftDeleteOperation(_currentUtcTimeProvider));
            Guid createdTrainId = (await trainListRepository.GetManyAsync(user1Id)).First(t => t.Name == trainName).Id;
            TrainOutputModel? actual = await _repository.GetOneAsync(createdTrainId, user1Id);

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);

            Train? createdEntity = await FindTrainByNameAsync(trainName, user1Id);
            createdEntity.Should().NotBeNull();
            createdEntity?.CreatedBy.Should().Be(user1Id);
            createdEntity?.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, timeDelta);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTrain()
        {
            (Guid[] vehicleIds, Guid[] trainIds, string user1Id, _) = await AddTestEntitiesToDbAsync();
            string trainName = "Test Train - update";

            TrainOutputModel expected = new()
            {
                Name = trainName,
                Description = "Mixed train",
                MaxPullForce = 250,
                TrainVehicles =
                [
                    new() { VehicleId = vehicleIds[0], VehicleName = "Test Vehicle 1", VehicleCount = 1, Position = 0, IsActive = true },
                    new() { VehicleId = vehicleIds[1], VehicleName = "Test Vehicle 2", VehicleCount = 3, Position = 1 },
                    new() { VehicleId = vehicleIds[3], VehicleName = "Test Vehicle 4", VehicleCount = 2, Position = 2 },
                    new() { VehicleId = vehicleIds[0], VehicleName = "Test Vehicle 1", VehicleCount = 1, Position = 3 }
                ]
            };

            TrainInputModel newModel = new()
            {
                Name = trainName,
                Description = "Mixed train",
                MaxPullForce = 250,
                TrainVehicles =
                [
                    new() { VehicleId = vehicleIds[0], VehicleCount = 1, Position = 0, IsActive = true },
                    new() { VehicleId = vehicleIds[1], VehicleCount = 3, Position = 1 },
                    new() { VehicleId = vehicleIds[3], VehicleCount = 2, Position = 2 },
                    new() { VehicleId = vehicleIds[0], VehicleCount = 1, Position = 3 }
                ]
            };

            await _repository.UpdateAsync(trainIds[0], newModel, user1Id);
            TrainOutputModel? actual = await _repository.GetOneAsync(trainIds[0], user1Id);

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);

            Train? updatedEntity = await FindTrainByNameAsync(trainName, user1Id);
            updatedEntity.Should().NotBeNull();
            updatedEntity?.UpdatedBy.Should().Be(user1Id);
            updatedEntity?.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, timeDelta);
        }
    }
}
