using Application.Features.RailVehicles.Model;
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
    public class RailVehicleRepositoryTests : RailVehicleIntegrationTestsBase
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider;
        private readonly IInsertOperation _insertOperation;
        private readonly IUpdateOperation _updateOperation;
        private readonly RailVehicleRepository _repository;

        public RailVehicleRepositoryTests(DatabaseFixture databaseFixture)
            : base(databaseFixture)
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
            _currentUtcTimeProvider = new CurrentUtcTimeProvider();
            _insertOperation = new InsertOperation(_mapper, _currentUtcTimeProvider);
            _updateOperation = new UpdateOperation(_mapper, _currentUtcTimeProvider);
            _repository = new RailVehicleRepository(_mapper, _dbContext, _insertOperation, _updateOperation);
        }

        private async Task GetOneAsync_ShouldReturnVehicle<T>(T expected, int vehicleIdIndex)
            where T : RailVehicleModelBase
        {
            (Guid[] vehicleIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            T? actual = await _repository.GetOneAsync(vehicleIds[vehicleIdIndex], user1Id) as T;

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnDependentVehicle()
        {
            RailVehicleDependentModel expected = new()
            {
                Name = "Test Vehicle 1",
                Description = "Dependent",
                Weight = 90,
                Length = 20,
                Wheelsets = 4,
                EquivalentRotatingWeight = 25,
                MaxSpeed = 200,
                ResistanceConstant = 0.5,
                ResistanceLinear = 0.1,
                ResistanceQuadratic = 0.0002,
                DrivingWheelsets = 4,
                Performance = 6400,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Speed = 0, PullForce = 300 },
                    new() { Speed = 100, PullForce = 250 },
                    new() { Speed = 200, PullForce = 100 },
                ],
                Efficiency = 0.9
            };

            await GetOneAsync_ShouldReturnVehicle(expected, 0);
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnPulledVehicle()
        {
            RailVehiclePulledModel expected = new()
            {
                Name = "Test Vehicle 2",
                Description = "Pulled",
                Weight = 90,
                Length = 20,
                Wheelsets = 4,
                EquivalentRotatingWeight = 25,
                MaxSpeed = 200,
                ResistanceConstant = 0.5,
                ResistanceLinear = 0.1,
                ResistanceQuadratic = 0.0002
            };

            await GetOneAsync_ShouldReturnVehicle(expected, 1);
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnIndependentVehicle()
        {
            RailVehicleIndependentModel expected = new()
            {
                Name = "Test Vehicle 3",
                Description = "Independent",
                Weight = 90,
                Length = 20,
                Wheelsets = 4,
                EquivalentRotatingWeight = 25,
                MaxSpeed = 100,
                ResistanceConstant = 0.5,
                ResistanceLinear = 0.1,
                ResistanceQuadratic = 0.0002,
                DrivingWheelsets = 4,
                Performance = 3200,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Speed = 0, PullForce = 300 },
                    new() { Speed = 100, PullForce = 250 },
                    new() { Speed = 200, PullForce = 100 },
                ],
                Efficiency = 0.9
            };

            await GetOneAsync_ShouldReturnVehicle(expected, 2);
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnHybridVehicle()
        {
            RailVehicleHybridModel expected = new()
            {
                Name = "Test Vehicle 4",
                Description = "Hybrid",
                Weight = 90,
                Length = 20,
                Wheelsets = 4,
                EquivalentRotatingWeight = 25,
                MaxSpeed = 100,
                ResistanceConstant = 0.5,
                ResistanceLinear = 0.1,
                ResistanceQuadratic = 0.0002,
                DrivingWheelsets = 4,
                Performance = 3200,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Speed = 0, PullForce = 300 },
                    new() { Speed = 100, PullForce = 250 },
                    new() { Speed = 200, PullForce = 100 },
                ],
                MaxSpeedHybrid = 90,
                PerformanceHybrid = 1600,
                EfficiencyDependent = 0.9,
                EfficiencyIndependent = 0.8
            };

            await GetOneAsync_ShouldReturnVehicle(expected, 3);
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnNull_WhenVehicleDoesNotExist()
        {
            (_, string user1Id, _) = await AddTestEntitiesToDbAsync();

            RailVehicleDependentModel? actual = await _repository.GetOneAsync(Guid.NewGuid(), user1Id) as RailVehicleDependentModel;

            actual.Should().BeNull();
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnNull_WhenVehicleBelongsToAnotherUser()
        {
            (Guid[] vehicleIds, _, string user2Id) = await AddTestEntitiesToDbAsync();

            RailVehicleDependentModel? actual = await _repository.GetOneAsync(vehicleIds[0], user2Id) as RailVehicleDependentModel;

            actual.Should().BeNull();
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnNull_WhenVehicleIsDeleted()
        {
            (Guid[] vehicleIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            RailVehicleDependentModel? actual = await _repository.GetOneAsync(vehicleIds[4], user1Id) as RailVehicleDependentModel;

            actual.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_ShouldInsertVehicle()
        {
            (_, string user1Id, _) = await AddTestEntitiesToDbAsync();
            string vehicleName = "Test Vehicle - create";

            RailVehiclePulledModel expected = new()
            {
                Name = vehicleName,
                Description = "Pulled",
                Weight = 40,
                Length = 25,
                Wheelsets = 4,
                EquivalentRotatingWeight = 2,
                MaxSpeed = 160,
                ResistanceConstant = 0.4,
                ResistanceLinear = 0.05,
                ResistanceQuadratic = 0.0001
            };

            await _repository.CreateAsync(expected, user1Id);

            SoftDeleteOperation softDeleteOperation = new(_currentUtcTimeProvider);
            RailVehicleListRepository vehicleListRepository = new(_mapper, _dbContext, softDeleteOperation);
            Guid createdVehicleId = (await vehicleListRepository.GetManyAsync(user1Id)).First(v => v.Name == vehicleName).Id;
            RailVehiclePulledModel? actual = await _repository.GetOneAsync(createdVehicleId, user1Id) as RailVehiclePulledModel;

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);

            RailVehicle? createdEntity = await FindVehicleByNameAsync(vehicleName, user1Id);
            createdEntity.Should().NotBeNull();
            createdEntity?.CreatedBy.Should().Be(user1Id);
            createdEntity?.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, timeDelta);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateVehicle()
        {
            (Guid[] vehicleIds, string user1Id, _) = await AddTestEntitiesToDbAsync();
            string vehicleName = "Test Vehicle - update";

            RailVehicleDependentModel expected = new()
            {
                Name = vehicleName,
                Description = "Updated Dependent vehicle",
                Weight = 100,
                Length = 25,
                Wheelsets = 6,
                EquivalentRotatingWeight = 30,
                MaxSpeed = 250,
                ResistanceConstant = 0.6,
                ResistanceLinear = 0.2,
                ResistanceQuadratic = 0.0004,
                DrivingWheelsets = 6,
                Performance = 8000,
                MaxPullForce = 400,
                TractionDiagram = [
                    new() { Speed = 0, PullForce = 400 },
                    new() { Speed = 200, PullForce = 150 }
                ],
                Efficiency = 0.95
            };

            await _repository.UpdateAsync(vehicleIds[0], expected, user1Id);
            RailVehicleDependentModel? actual = await _repository.GetOneAsync(vehicleIds[0], user1Id) as RailVehicleDependentModel;

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);

            RailVehicle? updatedEntity = await FindVehicleByNameAsync(vehicleName, user1Id);
            updatedEntity.Should().NotBeNull();
            updatedEntity?.UpdatedBy.Should().Be(user1Id);
            updatedEntity?.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, timeDelta);
        }
    }
}
