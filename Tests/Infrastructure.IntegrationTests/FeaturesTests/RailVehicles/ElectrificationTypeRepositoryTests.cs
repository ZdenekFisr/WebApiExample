using Application.Features.RailVehicles.Model;
using Application.Helpers;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.DatabaseOperations.HardDelete;
using Infrastructure.DatabaseOperations.Insert;
using Infrastructure.DatabaseOperations.Update;
using Infrastructure.Features.RailVehicles.Repository;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.IntegrationTests.FeaturesTests.RailVehicles
{
    [Collection("Database")]
    public class ElectrificationTypeRepositoryTests : IAsyncLifetime
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Func<Task> _resetDatabase;

        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider;
        private readonly IInsertOperation _insertOperation;
        private readonly IUpdateOperation _updateOperation;
        private readonly IHardDeleteOperation _hardDeleteOperation;
        private readonly ElectrificationTypeRepository _repository;

        private readonly EntityProvider _entityProvider = new();

        private readonly TimeSpan offset = new(0);
        private readonly TimeSpan timeDelta = TimeSpan.FromSeconds(5);

        public ElectrificationTypeRepositoryTests(DatabaseFixture databaseFixture)
        {
            _resetDatabase = databaseFixture.ResetDatabase;

            _dbContext = databaseFixture.Context;
            _currentUtcTimeProvider = new CurrentUtcTimeProvider();
            _insertOperation = new InsertOperation(_currentUtcTimeProvider);
            _updateOperation = new UpdateOperation(_currentUtcTimeProvider);
            _hardDeleteOperation = new HardDeleteOperation();
            _repository = new ElectrificationTypeRepository(_dbContext, _insertOperation, _updateOperation, _hardDeleteOperation);
        }
        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync() => await _resetDatabase();

        private async Task<ElectrificationType?> FindElectrificationTypeByNameAsync(string name, string userId)
            => await _dbContext.ElectrificationTypes
                .FirstOrDefaultAsync(e => e.Name == name && e.UserId == userId);

        private async Task<(Guid[] ids, string user1Id, string user2Id)> AddTestEntitiesToDbAsync()
        {
            Guid[] ids = GuidHelpers.GenerateRandomGuids(4).ToArray();
            string user1Id = Guid.NewGuid().ToString();
            string user2Id = Guid.NewGuid().ToString();

            ElectrificationType[] testElectrificationTypes = _entityProvider.GetElectrificationTypes(ids, user1Id, user2Id);
            await _dbContext.Users.AddRangeAsync(_entityProvider.GetUsers(user1Id, user2Id));
            await _dbContext.ElectrificationTypes.AddRangeAsync(testElectrificationTypes);

            await _dbContext.SaveChangesAsync();

            return (ids, user1Id, user2Id);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnElectrificationTypes()
        {
            (Guid[] ids, string user1Id, _) = await AddTestEntitiesToDbAsync();

            ICollection<ElectrificationTypeListModel> expected =
            [
                new() { Id = ids[0], Name = "Test Electrification Type 1", Description = "DC overhead", Voltage = 3000, CreatedAt = new(2024, 12, 7, 21, 55, 46, offset) },
                new() { Id = ids[1], Name = "Test Electrification Type 2", Description = "AC overhead", Voltage = 25000, CreatedAt = new(2024, 12, 7, 22, 0, 13, offset), UpdatedAt = new(2024, 12, 7, 22, 4, 50, offset) }
            ];

            ICollection<ElectrificationTypeListModel> actual = await _repository.GetManyAsync(user1Id);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task CreateAsync_ShouldInsertElectrificationType()
        {
            (_, string user1Id, _) = await AddTestEntitiesToDbAsync();
            string name = "Test Electrification Type - C";

            ElectrificationTypeModel expected = new()
            {
                Name = name,
                Description = "DC overhead",
                Voltage = 1500
            };

            await _repository.CreateAsync(expected, user1Id);

            ElectrificationTypeModel? actual = (await _repository.GetManyAsync(user1Id))
                .Select(e => new ElectrificationTypeModel { Name = e.Name, Description = e.Description, Voltage = e.Voltage })
                .FirstOrDefault(e => e.Name == expected.Name);

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);

            ElectrificationType? createdEntity = await FindElectrificationTypeByNameAsync(name, user1Id);
            createdEntity.Should().NotBeNull();
            createdEntity?.CreatedBy.Should().Be(user1Id);
            createdEntity?.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, timeDelta);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateElectrificationType()
        {
            (Guid[] ids, string user1Id, _) = await AddTestEntitiesToDbAsync();
            string name = "Test Electrification Type - U";

            ElectrificationTypeModel expected = new()
            {
                Name = name,
                Description = "AC 16,7 Hz overhead",
                Voltage = 15000
            };

            await _repository.UpdateAsync(ids[1], expected, user1Id);

            ElectrificationTypeModel? actual = (await _repository.GetManyAsync(user1Id))
                .Select(e => new ElectrificationTypeModel { Name = e.Name, Description = e.Description, Voltage = e.Voltage })
                .FirstOrDefault(e => e.Name == expected.Name);

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);

            ElectrificationType? updatedEntity = await FindElectrificationTypeByNameAsync(name, user1Id);
            updatedEntity.Should().NotBeNull();
            updatedEntity?.UpdatedBy.Should().Be(user1Id);
            updatedEntity?.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, timeDelta);
        }

        [Fact]
        public async Task HardDeleteAsync_ShouldHardDeleteElectrificationType()
        {
            (Guid[] ids, string user1Id, _) = await AddTestEntitiesToDbAsync();

            await _repository.HardDeleteAsync(ids[1], user1Id);

            ElectrificationType? deletedEntity = await FindElectrificationTypeByNameAsync("Test Electrification Type 2", user1Id);
            deletedEntity.Should().BeNull();
        }
    }
}
