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

        [Fact]
        public async Task GetOneAsync_ShouldReturnDependentVehicle()
        {
            (Guid[] vehicleIds, Guid[] elTypeIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            RailVehicleDrivingModel expected = new()
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
                TractionSystems = [
                    new VehicleTractionSystemModel
                    {
                        ElectrificationTypeId = elTypeIds[0],
                        VoltageCoefficient = 1,
                        DrivingWheelsets = 4,
                        MaxSpeed = 200,
                        Performance = 6400,
                        MaxPullForce = 300,
                        Efficiency = 0.9,
                        TractionDiagram = [
                            new() { Speed = 0, PullForce = 300 },
                            new() { Speed = 100, PullForce = 250 },
                            new() { Speed = 200, PullForce = 100 }
                        ]
                    }
                ]
            };

            RailVehicleDrivingModel? actual = await _repository.GetOneAsync(vehicleIds[0], user1Id) as RailVehicleDrivingModel;

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnPulledVehicle()
        {
            (Guid[] vehicleIds, _, string user1Id, _) = await AddTestEntitiesToDbAsync();

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

            RailVehiclePulledModel? actual = await _repository.GetOneAsync(vehicleIds[1], user1Id) as RailVehiclePulledModel;

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnIndependentVehicle()
        {
            (Guid[] vehicleIds, _, string user1Id, _) = await AddTestEntitiesToDbAsync();

            RailVehicleDrivingModel expected = new()
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
                TractionSystems = [
                    new VehicleTractionSystemModel
                    {
                        ElectrificationTypeId = null,
                        VoltageCoefficient = null,
                        DrivingWheelsets = 4,
                        MaxSpeed = 100,
                        Performance = 3200,
                        MaxPullForce = 300,
                        Efficiency = 0.9,
                        TractionDiagram = [
                            new() { Speed = 0, PullForce = 300 },
                            new() { Speed = 100, PullForce = 250 },
                            new() { Speed = 200, PullForce = 100 }
                        ]
                    }
                ]
            };

            RailVehicleDrivingModel? actual = await _repository.GetOneAsync(vehicleIds[2], user1Id) as RailVehicleDrivingModel;

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnHybridVehicle()
        {
            (Guid[] vehicleIds, Guid[] elTypeIds, string user1Id, _) = await AddTestEntitiesToDbAsync();

            RailVehicleDrivingModel expected = new()
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
                TractionSystems = [
                    new VehicleTractionSystemModel
                    {
                        ElectrificationTypeId = null,
                        VoltageCoefficient = null,
                        DrivingWheelsets = 4,
                        MaxSpeed = 90,
                        Performance = 1600,
                        MaxPullForce = 250,
                        Efficiency = 0.8,
                        TractionDiagram = []
                    },
                    new VehicleTractionSystemModel
                    {
                        ElectrificationTypeId = elTypeIds[0],
                        VoltageCoefficient = 1,
                        DrivingWheelsets = 4,
                        MaxSpeed = 100,
                        Performance = 3200,
                        MaxPullForce = 300,
                        Efficiency = 0.9,
                        TractionDiagram = [
                            new() { Speed = 0, PullForce = 300 },
                            new() { Speed = 100, PullForce = 250 },
                            new() { Speed = 200, PullForce = 100 }
                        ]
                    }
                ]
            };

            RailVehicleDrivingModel? actual = await _repository.GetOneAsync(vehicleIds[3], user1Id) as RailVehicleDrivingModel;

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnNull_WhenVehicleDoesNotExist()
        {
            (_, _, string user1Id, _) = await AddTestEntitiesToDbAsync();

            RailVehicleDrivingModel? actual = await _repository.GetOneAsync(Guid.NewGuid(), user1Id) as RailVehicleDrivingModel;

            actual.Should().BeNull();
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnNull_WhenVehicleBelongsToAnotherUser()
        {
            (Guid[] vehicleIds, _, _, string user2Id) = await AddTestEntitiesToDbAsync();

            RailVehicleDrivingModel? actual = await _repository.GetOneAsync(vehicleIds[0], user2Id) as RailVehicleDrivingModel;

            actual.Should().BeNull();
        }

        [Fact]
        public async Task GetOneAsync_ShouldReturnNull_WhenVehicleIsDeleted()
        {
            (Guid[] vehicleIds, _, string user1Id, _) = await AddTestEntitiesToDbAsync();

            RailVehicleDrivingModel? actual = await _repository.GetOneAsync(vehicleIds[4], user1Id) as RailVehicleDrivingModel;

            actual.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_ShouldInsertVehicle()
        {
            (_, _, string user1Id, _) = await AddTestEntitiesToDbAsync();
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

            RailVehicleListRepository vehicleListRepository = new(_mapper, _dbContext, new SoftDeleteOperation(_currentUtcTimeProvider));
            Guid createdVehicleId = (await vehicleListRepository.GetPulledVehiclesAsync(user1Id)).First(v => v.Name == vehicleName).Id;
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
            (Guid[] vehicleIds, Guid[] elTypeIds, string user1Id, _) = await AddTestEntitiesToDbAsync();
            string vehicleName = "Test Vehicle - update";

            RailVehicleDrivingModel expected = new()
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
                TractionSystems = [
                    new VehicleTractionSystemModel
                    {
                        ElectrificationTypeId = elTypeIds[1],
                        VoltageCoefficient = 0.8,
                        DrivingWheelsets = 6,
                        MaxSpeed = 250,
                        Performance = 8000,
                        MaxPullForce = 400,
                        Efficiency = 0.95,
                        TractionDiagram = [
                            new() { Speed = 0, PullForce = 400 },
                            new() { Speed = 200, PullForce = 150 }
                        ]
                    }
                ]
            };

            await _repository.UpdateAsync(vehicleIds[0], expected, user1Id);
            RailVehicleDrivingModel? actual = await _repository.GetOneAsync(vehicleIds[0], user1Id) as RailVehicleDrivingModel;

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);

            RailVehicle? updatedEntity = await FindVehicleByNameAsync(vehicleName, user1Id);
            updatedEntity.Should().NotBeNull();
            updatedEntity?.UpdatedBy.Should().Be(user1Id);
            updatedEntity?.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, timeDelta);
        }
    }
}
