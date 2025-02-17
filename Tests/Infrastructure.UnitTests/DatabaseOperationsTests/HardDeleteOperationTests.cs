using Domain.Common;
using FluentAssertions;
using Infrastructure.DatabaseOperations.HardDelete;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitTests.DatabaseOperationsTests
{
    public class HardDeleteOperationTests
    {
        private readonly HardDeleteOperation _hardDeleteOperation;
        private readonly TestHardDeleteDbContext _dbContext;

        public HardDeleteOperationTests()
        {
            _hardDeleteOperation = new HardDeleteOperation();

            var options = new DbContextOptionsBuilder<TestHardDeleteDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new TestHardDeleteDbContext(options);
        }

        [Fact]
        public async Task HardDeleteAsync_UserIdMatches_DeletesEntity()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var userId = "test-user";
            var entity = new TestHardDeleteEntity
            {
                Id = entityId,
                UserId = userId,
                Name = "Test Entity"
            };

            await _dbContext.TestEntities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            // Act
            await _hardDeleteOperation.HardDeleteAsync<TestHardDeleteEntity>(_dbContext, entityId, userId);

            // Assert
            var deletedEntity = await _dbContext.TestEntities.FindAsync(entityId);
            deletedEntity.Should().BeNull();
        }

        [Fact]
        public async Task HardDeleteAsync_EntityDoesNotExist_DoesNotDeleteEntity()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var userId = "test-user";
            var entity = new TestHardDeleteEntity
            {
                Id = entityId,
                UserId = userId,
                Name = "Test Entity"
            };

            await _dbContext.TestEntities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            // Act
            await _hardDeleteOperation.HardDeleteAsync<TestHardDeleteEntity>(_dbContext, Guid.NewGuid(), userId);

            // Assert
            var deletedEntity = await _dbContext.TestEntities.FindAsync(entityId);
            deletedEntity.Should().NotBeNull();
        }

        [Fact]
        public async Task HardDeleteAsync_UserIdDoesNotMatch_DoesNotDeleteEntity()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var userId = "test-user";
            var entity = new TestHardDeleteEntity
            {
                Id = entityId,
                UserId = userId,
                Name = "Test Entity"
            };

            await _dbContext.TestEntities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            // Act
            await _hardDeleteOperation.HardDeleteAsync<TestHardDeleteEntity>(_dbContext, entityId, "other-test-user");

            // Assert
            var deletedEntity = await _dbContext.TestEntities.FindAsync(entityId);
            deletedEntity.Should().NotBeNull();
        }

        private class TestHardDeleteDbContext(DbContextOptions options) : DbContext(options)
        {
            public DbSet<TestHardDeleteEntity> TestEntities { get; set; }
        }

        private class TestHardDeleteEntity : EntityWithUserBase
        {
            public required string Name { get; set; }
        }
    }
}
