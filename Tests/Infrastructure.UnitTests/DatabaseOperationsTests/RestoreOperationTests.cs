using FluentAssertions;
using Infrastructure.DatabaseOperations.Restore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitTests.DatabaseOperationsTests
{
    public class RestoreOperationTests
    {
        private readonly RestoreOperation<TestSoftDeletableEntity> _restoreOperation;
        private readonly TestSoftDeletableDbContext _dbContext;

        public RestoreOperationTests()
        {
            _restoreOperation = new RestoreOperation<TestSoftDeletableEntity>();

            var options = new DbContextOptionsBuilder<TestSoftDeletableDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new TestSoftDeletableDbContext(options);
        }

        [Fact]
        public async Task RestoreAsync_EntityExistsAndIsSoftDeletable_RestoresEntity()
        {
            // Arrange
            var userId = "test-user-id";
            var id = Guid.NewGuid();
            var entity = new TestSoftDeletableEntity
            {
                Id = id,
                UserId = userId,
                IsDeleted = true,
                DeletedBy = userId,
                DeletedAt = DateTimeOffset.UtcNow
            };

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            // Act
            await _restoreOperation.RestoreAsync(_dbContext, entity.Id, userId);

            // Assert
            var actualEntity = await _dbContext.Set<TestSoftDeletableEntity>().FindAsync(entity.Id);
            actualEntity.Should().NotBeNull();
            actualEntity?.IsDeleted.Should().BeFalse();
            actualEntity?.DeletedBy.Should().Be(userId);
            actualEntity?.DeletedAt.Should().Be(entity.DeletedAt);
        }

        [Fact]
        public async Task RestoreAsync_EntityDoesNotExist_DoesNothing()
        {
            // Arrange
            var userId = "test-user-id";
            var id = Guid.NewGuid();

            // Act
            await _restoreOperation.RestoreAsync(_dbContext, id, userId);

            // Assert
            _dbContext.ChangeTracker.Entries().Should().BeEmpty();
        }

        [Fact]
        public async Task RestoreAsync_EntityIsNotOwnedByUser_DoesNothing()
        {
            // Arrange
            var userId = "test-user-id";
            var id = Guid.NewGuid();
            var entity = new TestSoftDeletableEntity
            {
                Id = id,
                UserId = userId,
                IsDeleted = true,
                DeletedBy = userId,
                DeletedAt = DateTimeOffset.UtcNow
            };

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            // Act
            await _restoreOperation.RestoreAsync(_dbContext, entity.Id, "another-user-id");

            // Assert
            var actualEntity = await _dbContext.Set<TestSoftDeletableEntity>().FindAsync(id);
            actualEntity?.IsDeleted.Should().BeTrue();
        }
    }
}
