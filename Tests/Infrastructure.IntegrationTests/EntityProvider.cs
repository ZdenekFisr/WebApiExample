using Application.Helpers;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.IntegrationTests
{
    public class EntityProvider
    {
        private readonly TimeSpan offset = new(0);

        public User[] GetUsers(string user1Id, string user2Id)
            =>
        [
            new() { Id = user1Id, UserName = "User 1", Email="user1@gmail.com", PasswordHash = "abc" },
            new() { Id = user2Id, UserName = "User 2", Email="user2@gmail.com", PasswordHash = "def" }
        ];

        public (RailVehicle[], Train[]) GetTestVehiclesAndTrains([MinLength(8)] Guid[] vehicleIds, [MinLength(1)] Guid[] elTypeIds, [MinLength(6)] Guid[] trainIds, string user1Id, string user2Id)
        {
            RailVehicle[] vehicles = GetTestVehicles(vehicleIds, elTypeIds, user1Id, user2Id);

            Train[] trains =
            [
                new()
                {
                    Id = trainIds[0],
                    UserId = user1Id,
                    Name = "Test Train 1",
                    Description = "Mixed",
                    MaxPullForce = 300,
                    TrainVehicles = [
                        new() { Id = Guid.NewGuid(), VehicleId = vehicleIds[0], VehicleCount = 1, Position = 0, IsActive = true, TrainId = trainIds[0] },
                        new() { Id = Guid.NewGuid(), VehicleId = vehicleIds[1], VehicleCount = 3, Position = 1, TrainId = trainIds[0] },
                        new() { Id = Guid.NewGuid(), VehicleId = vehicleIds[2], VehicleCount = 2, Position = 2, TrainId = trainIds[0] },
                        new() { Id = Guid.NewGuid(), VehicleId = vehicleIds[3], VehicleCount = 2, Position = 3, IsActive = true, TrainId = trainIds[0] }
                    ],
                    CreatedAt = new(2024, 12, 6, 19, 34, 12, offset),
                    CreatedBy = user1Id
                },
                new()
                {
                    Id = trainIds[1],
                    UserId = user1Id,
                    Name = "Test Train 2",
                    Description = "With deleted vehicle",
                    MaxPullForce = 200,
                    TrainVehicles = [
                        new() { Id = Guid.NewGuid(), VehicleId = vehicleIds[0], VehicleCount = 1, Position = 0, IsActive = true, TrainId = trainIds[1] },
                        new() { Id = Guid.NewGuid(), VehicleId = vehicleIds[4], VehicleCount = 3, Position = 1, TrainId = trainIds[1] }
                    ],
                    CreatedAt = new(2024, 12, 6, 19, 37, 33, offset),
                    CreatedBy = user1Id,
                    UpdatedAt = new(2024, 12, 6, 22, 50, 4, offset),
                    UpdatedBy = user1Id
                },
                new()
                {
                    Id = trainIds[2],
                    UserId = user2Id,
                    Name = "Test Train 4",
                    Description = "Mixed user 2",
                    MaxPullForce = 300,
                    TrainVehicles = [
                        new() { Id = Guid.NewGuid(), VehicleId = vehicleIds[6], VehicleCount = 1, Position = 0, IsActive = true, TrainId = trainIds[3] },
                        new() { Id = Guid.NewGuid(), VehicleId = vehicleIds[7], VehicleCount = 3, Position = 1, TrainId = trainIds[3] }
                    ],
                    CreatedAt = new(2024, 12, 6, 19, 47, 20, offset),
                    CreatedBy = user2Id
                },
                new()
                {
                    Id = trainIds[3],
                    UserId = user1Id,
                    Name = "Test Train 5",
                    Description = "Empty",
                    MaxPullForce = 300,
                    TrainVehicles = [],
                    CreatedAt = new(2024, 12, 6, 20, 0, 47, offset),
                    CreatedBy = user1Id
                },
                new()
                {
                    Id = trainIds[4],
                    UserId = user1Id,
                    Name = "Test Train 6",
                    Description = "Deleted",
                    MaxPullForce = 300,
                    TrainVehicles = [],
                    CreatedAt = new(2024, 12, 6, 20, 7, 3, offset),
                    CreatedBy = user1Id,
                    IsDeleted = true,
                    DeletedAt = new(2024, 12, 6, 22, 51, 54, offset),
                    DeletedBy = user1Id
                }
            ];
            return (vehicles, trains);
        }

        public RailVehicle[] GetTestVehicles([MinLength(8)] Guid[] vehicleIds, [MinLength(1)] Guid[] elTypeIds, string user1Id, string user2Id)
        {
            Guid[] tractionSystemIds = GuidHelpers.GenerateRandomGuids(6).ToArray();

            RailVehicle[] vehicles =
            [
                new()
                {
                    Id = vehicleIds[0],
                    UserId = user1Id,
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
                        new()
                        {
                            Id = tractionSystemIds[0],
                            VehicleId = vehicleIds[0],
                            ElectrificationTypeId = elTypeIds[0],
                            VoltageCoefficient = 1,
                            DrivingWheelsets = 4,
                            MaxSpeed = 200,
                            Performance = 6400,
                            MaxPullForce = 300,
                            Efficiency = 0.9,
                            TractionDiagram = [
                                new() { Id = Guid.NewGuid(), Speed = 0, PullForce = 300, VehicleTractionSystemId = tractionSystemIds[0] },
                                new() { Id = Guid.NewGuid(), Speed = 100, PullForce = 250, VehicleTractionSystemId = tractionSystemIds[0] },
                                new() { Id = Guid.NewGuid(), Speed = 200, PullForce = 100, VehicleTractionSystemId = tractionSystemIds[0] }
                            ]
                        }
                    ],
                    CreatedAt = new(2024, 12, 6, 16, 32, 51, offset),
                    CreatedBy = user1Id
                },
                new()
                {
                    Id = vehicleIds[1],
                    UserId = user1Id,
                    Name = "Test Vehicle 2",
                    Description = "Pulled",
                    Weight = 90,
                    Length = 20,
                    Wheelsets = 4,
                    EquivalentRotatingWeight = 25,
                    MaxSpeed = 200,
                    ResistanceConstant = 0.5,
                    ResistanceLinear = 0.1,
                    ResistanceQuadratic = 0.0002,
                    TractionSystems = [],
                    CreatedAt = new(2024, 12, 6, 16, 35, 6, offset),
                    CreatedBy = user1Id,
                    UpdatedAt = new(2024, 12, 6, 16, 35, 55, offset),
                    UpdatedBy = user1Id
                },
                new()
                {
                    Id = vehicleIds[2],
                    UserId = user1Id,
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
                        new()
                        {
                            Id = tractionSystemIds[1],
                            VehicleId = vehicleIds[2],
                            ElectrificationTypeId = null,
                            VoltageCoefficient = null,
                            DrivingWheelsets = 4,
                            MaxSpeed = 100,
                            Performance = 3200,
                            MaxPullForce = 300,
                            Efficiency = 0.9,
                            TractionDiagram = [
                                new() { Id = Guid.NewGuid(), Speed = 0, PullForce = 300, VehicleTractionSystemId = tractionSystemIds[1] },
                                new() { Id = Guid.NewGuid(), Speed = 100, PullForce = 250, VehicleTractionSystemId = tractionSystemIds[1] },
                                new() { Id = Guid.NewGuid(), Speed = 200, PullForce = 100, VehicleTractionSystemId = tractionSystemIds[1] }
                            ],
                        }
                    ],

                    CreatedAt = new(2024, 12, 6, 16, 39, 32, offset),
                    CreatedBy = user1Id,
                    UpdatedAt = new(2024, 12, 6, 16, 40, 47, offset),
                    UpdatedBy = user1Id
                },
                new()
                {
                    Id = vehicleIds[3],
                    UserId = user1Id,
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
                        new()
                        {
                            Id = tractionSystemIds[2],
                            VehicleId = vehicleIds[3],
                            ElectrificationTypeId = null,
                            VoltageCoefficient = null,
                            DrivingWheelsets = 4,
                            MaxSpeed = 90,
                            Performance = 1600,
                            MaxPullForce = 250,
                            Efficiency = 0.8,
                            TractionDiagram = []
                        },
                        new()
                        {
                            Id = tractionSystemIds[3],
                            VehicleId = vehicleIds[3],
                            ElectrificationTypeId = elTypeIds[0],
                            VoltageCoefficient = 1,
                            DrivingWheelsets = 4,
                            MaxSpeed = 100,
                            Performance = 3200,
                            MaxPullForce = 300,
                            Efficiency = 0.9,
                            TractionDiagram = [
                                new() { Id = Guid.NewGuid(), Speed = 0, PullForce = 300, VehicleTractionSystemId = tractionSystemIds[3] },
                                new() { Id = Guid.NewGuid(), Speed = 100, PullForce = 250, VehicleTractionSystemId = tractionSystemIds[3] },
                                new() { Id = Guid.NewGuid(), Speed = 200, PullForce = 100, VehicleTractionSystemId = tractionSystemIds[3] }
                            ]
                        }
                    ],
                    CreatedAt = new(2024, 12, 6, 16, 45, 2, offset),
                    CreatedBy = user1Id
                },
                new()
                {
                    Id = vehicleIds[4],
                    UserId = user1Id,
                    Name = "Test Vehicle 5",
                    Description = "Dependent deleted",
                    Weight = 90,
                    Length = 20,
                    Wheelsets = 4,
                    EquivalentRotatingWeight = 25,
                    MaxSpeed = 200,
                    ResistanceConstant = 0.5,
                    ResistanceLinear = 0.1,
                    ResistanceQuadratic = 0.0002,
                    TractionSystems = [
                        new()
                        {
                            Id = tractionSystemIds[4],
                            VehicleId = vehicleIds[4],
                            ElectrificationTypeId = elTypeIds[0],
                            VoltageCoefficient = 1,
                            DrivingWheelsets = 4,
                            MaxSpeed = 200,
                            Performance = 6400,
                            MaxPullForce = 300,
                            Efficiency = 0.9,
                            TractionDiagram = [
                                new() { Id = Guid.NewGuid(), Speed = 0, PullForce = 300, VehicleTractionSystemId = tractionSystemIds[4] },
                                new() { Id = Guid.NewGuid(), Speed = 100, PullForce = 250, VehicleTractionSystemId = tractionSystemIds[4] },
                                new() { Id = Guid.NewGuid(), Speed = 200, PullForce = 100, VehicleTractionSystemId = tractionSystemIds[4] }
                            ]
                        }
                    ],
                    CreatedAt = new(2024, 12, 6, 16, 52, 13, offset),
                    CreatedBy = user1Id,
                    IsDeleted = true,
                    DeletedAt = new(2024, 12, 6, 16, 58, 26, offset),
                    DeletedBy = user1Id
                },
                new()
                {
                    Id = vehicleIds[5],
                    UserId = user1Id,
                    Name = "Test Vehicle 6",
                    Description = "Pulled deleted",
                    Weight = 90,
                    Length = 20,
                    Wheelsets = 4,
                    EquivalentRotatingWeight = 25,
                    MaxSpeed = 200,
                    ResistanceConstant = 0.5,
                    ResistanceLinear = 0.1,
                    ResistanceQuadratic = 0.0002,
                    TractionSystems = [],
                    CreatedAt = new(2024, 12, 6, 16, 55, 10, offset),
                    CreatedBy = user1Id,
                    IsDeleted = true,
                    DeletedAt = new(2024, 12, 6, 16, 58, 55, offset),
                    DeletedBy = user1Id
                },
                new()
                {
                    Id = vehicleIds[6],
                    UserId = user2Id,
                    Name = "Test Vehicle 7",
                    Description = "Dependent user 2",
                    Weight = 90,
                    Length = 20,
                    Wheelsets = 4,
                    EquivalentRotatingWeight = 25,
                    MaxSpeed = 200,
                    ResistanceConstant = 0.5,
                    ResistanceLinear = 0.1,
                    ResistanceQuadratic = 0.0002,
                    TractionSystems = [
                        new()
                        {
                            Id = tractionSystemIds[5],
                            VehicleId = vehicleIds[6],
                            ElectrificationTypeId = elTypeIds[0],
                            VoltageCoefficient = 1,
                            DrivingWheelsets = 4,
                            MaxSpeed = 200,
                            Performance = 6400,
                            MaxPullForce = 300,
                            Efficiency = 0.9,
                            TractionDiagram = [
                                new() { Id = Guid.NewGuid(), Speed = 0, PullForce = 300, VehicleTractionSystemId = tractionSystemIds[5] },
                                new() { Id = Guid.NewGuid(), Speed = 100, PullForce = 250, VehicleTractionSystemId = tractionSystemIds[5] },
                                new() { Id = Guid.NewGuid(), Speed = 200, PullForce = 100, VehicleTractionSystemId = tractionSystemIds[5] }
                            ]
                        }
                    ],
                    CreatedAt = new(2024, 12, 6, 17, 26, 31, offset),
                    CreatedBy = user2Id
                },
                new()
                {
                    Id = vehicleIds[7],
                    UserId = user2Id,
                    Name = "Test Vehicle 8",
                    Description = "Pulled user 2",
                    Weight = 90,
                    Length = 20,
                    Wheelsets = 4,
                    EquivalentRotatingWeight = 25,
                    MaxSpeed = 200,
                    ResistanceConstant = 0.5,
                    ResistanceLinear = 0.1,
                    ResistanceQuadratic = 0.0002,
                    TractionSystems = [],
                    CreatedAt = new(2024, 12, 6, 17, 28, 17, offset),
                    CreatedBy = user2Id
                }
            ];
            return vehicles;
        }

        public ElectrificationType[] GetElectrificationTypes([MinLength(4)] Guid[] ids, string user1Id, string user2Id)
            =>
        [
            new()
            {
                Id = ids[0],
                UserId = user1Id,
                Name = "Test Electrification Type 1",
                Description = "DC overhead",
                Voltage = 3000,
                CreatedAt = new(2024, 12, 7, 21, 55, 46, offset),
                CreatedBy = user1Id
            },
            new()
            {
                Id = ids[1],
                UserId = user1Id,
                Name = "Test Electrification Type 2",
                Description = "AC overhead",
                Voltage = 25000,
                CreatedAt = new(2024, 12, 7, 22, 0, 13, offset),
                CreatedBy = user1Id,
                UpdatedAt = new(2024, 12, 7, 22, 4, 50, offset),
                UpdatedBy = user1Id
            },
            new()
            {
                Id = ids[2],
                UserId = user2Id,
                Name = "Test Electrification Type 3",
                Description = "DC third rail",
                Voltage = 750,
                CreatedAt = new(2024, 12, 7, 22, 7, 3, offset),
                CreatedBy = user2Id
            },
            new()
            {
                Id = ids[3],
                UserId = user1Id,
                Name = "Test Electrification Type 4",
                Description = "DC overhead - deleted",
                Voltage = 1500,
                CreatedAt = new(2024, 12, 7, 22, 12, 39, offset),
                CreatedBy = user1Id,
                IsDeleted = true,
                DeletedAt = new(2024, 12, 7, 22, 26, 21, offset),
                DeletedBy = user1Id
            }
        ];
    }
}
