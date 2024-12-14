using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.IntegrationTests
{
    internal class EntityProvider
    {
        private readonly TimeSpan offset = new(0);

        public RailVehicle[] GetTestVehicles([MinLength(8)] Guid[] vehicleIds, string user1Id, string user2Id)
            =>
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
                DrivingWheelsets = 4,
                Performance = 6400,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Id = Guid.NewGuid(), Speed = 0, PullForce = 300, RailVehicleId = vehicleIds[0] },
                    new() { Id = Guid.NewGuid(), Speed = 100, PullForce = 250, RailVehicleId = vehicleIds[0] },
                    new() { Id = Guid.NewGuid(), Speed = 200, PullForce = 100, RailVehicleId = vehicleIds[0] },
                ],
                EfficiencyDependent = 0.9,
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
                TractionDiagram = [],
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
                DrivingWheelsets = 4,
                Performance = 3200,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Id = Guid.NewGuid(), Speed = 0, PullForce = 300, RailVehicleId = vehicleIds[2] },
                    new() { Id = Guid.NewGuid(), Speed = 100, PullForce = 250, RailVehicleId = vehicleIds[2] },
                    new() { Id = Guid.NewGuid(), Speed = 200, PullForce = 100, RailVehicleId = vehicleIds[2] },
                ],
                EfficiencyIndependent = 0.9,
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
                DrivingWheelsets = 4,
                Performance = 3200,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Id = Guid.NewGuid(), Speed = 0, PullForce = 300, RailVehicleId = vehicleIds[3] },
                    new() { Id = Guid.NewGuid(), Speed = 100, PullForce = 250, RailVehicleId = vehicleIds[3] },
                    new() { Id = Guid.NewGuid(), Speed = 200, PullForce = 100, RailVehicleId = vehicleIds[3] },
                ],
                MaxSpeedHybrid = 90,
                PerformanceHybrid = 1600,
                EfficiencyDependent = 0.9,
                EfficiencyIndependent = 0.8,
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
                DrivingWheelsets = 4,
                Performance = 6400,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Id = Guid.NewGuid(), Speed = 0, PullForce = 300, RailVehicleId = vehicleIds[4] },
                    new() { Id = Guid.NewGuid(), Speed = 100, PullForce = 250, RailVehicleId = vehicleIds[4] },
                    new() { Id = Guid.NewGuid(), Speed = 200, PullForce = 100, RailVehicleId = vehicleIds[4] },
                ],
                EfficiencyDependent = 0.9,
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
                TractionDiagram = [],
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
                DrivingWheelsets = 4,
                Performance = 6400,
                MaxPullForce = 300,
                TractionDiagram = [
                    new() { Id = Guid.NewGuid(), Speed = 0, PullForce = 300, RailVehicleId = vehicleIds[6] },
                    new() { Id = Guid.NewGuid(), Speed = 100, PullForce = 250, RailVehicleId = vehicleIds[6] },
                    new() { Id = Guid.NewGuid(), Speed = 200, PullForce = 100, RailVehicleId = vehicleIds[6] },
                ],
                EfficiencyDependent = 0.9,
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
                TractionDiagram = [],
                CreatedAt = new(2024, 12, 6, 17, 28, 17, offset),
                CreatedBy = user2Id
            }
        ];
    }
}
