using FluentAssertions;
using Infrastructure.DatabaseOperations.SoftDelete;
using Infrastructure.Services.CurrentUtcTime;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.UnitTests.DatabaseOperationsTests
{
    public class SoftDeleteOperationTests
    {
        private readonly Mock<ICurrentUtcTimeProvider> _currentUtcTimeProviderMock;
        private readonly SoftDeleteOperation<TestSoftDeletableEntity> _softDeleteOperation;
        private readonly TestSoftDeletableDbContext _dbContext;

        public SoftDeleteOperationTests()
        {
            _currentUtcTimeProviderMock = new Mock<ICurrentUtcTimeProvider>();
            _softDeleteOperation = new SoftDeleteOperation<TestSoftDeletableEntity>(
                _currentUtcTimeProviderMock.Object);

            var options = new DbContextOptionsBuilder<TestSoftDeletableDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new TestSoftDeletableDbContext(options);
        }

        [Fact]
        public async Task SoftDeleteAsync_EntityExists_SetsIsDeletedToTrue()
        {
            // Arrange
            var userId = "test-user-id";
            var id = Guid.NewGuid();
            var currentTime = DateTimeOffset.UtcNow;
            var entity = new TestSoftDeletableEntity
            {
                Id = id,
                UserId = userId,
                IsDeleted = false
            };

            _currentUtcTimeProviderMock.Setup(provider => provider.GetCurrentUtcTime()).Returns(currentTime);

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            // Act
            await _softDeleteOperation.SoftDeleteAsync(_dbContext, id, userId);

            // Assert
            var actualEntity = await _dbContext.Set<TestSoftDeletableEntity>().FindAsync(entity.Id);
            actualEntity?.IsDeleted.Should().BeTrue();
            actualEntity?.DeletedAt.Should().BeCloseTo(currentTime, TimeSpan.FromSeconds(5));
            actualEntity?.DeletedBy.Should().Be(userId);
        }

        [Fact]
        public async Task SoftDeleteAsync_EntityDoesNotExist_DoesNothing()
        {
            // Arrange
            var userId = "test-user-id";
            var id = Guid.NewGuid();

            // Act
            await _softDeleteOperation.SoftDeleteAsync(_dbContext, id, userId);

            // Assert
            _dbContext.ChangeTracker.Entries().Should().BeEmpty();
        }

        [Fact]
        public async Task SoftDeleteAsync_EntityIsNotOwnedByUser_DoesNothing()
        {
            // Arrange
            var userId = "test-user-id";
            var id = Guid.NewGuid();
            var entity = new TestSoftDeletableEntity
            {
                Id = id,
                UserId = "another-user-id"
            };

            await _dbContext.Set<TestSoftDeletableEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            // Act
            await _softDeleteOperation.SoftDeleteAsync(_dbContext, id, userId);

            // Assert
            var actualEntity = await _dbContext.Set<TestSoftDeletableEntity>().FindAsync(id);
            actualEntity?.IsDeleted.Should().BeFalse();
        }
    }
}
