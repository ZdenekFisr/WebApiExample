using Application.Common;
using Application.Services;
using AutoMapper;
using Domain.Common;
using FluentAssertions;
using Infrastructure.DatabaseOperations.Update;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.UnitTests.DatabaseOperationsTests
{
    public class UpdateOperationTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICurrentUtcTimeProvider> _currentUtcTimeProviderMock;
        private readonly UpdateOperation _updateOperation;
        private readonly TestUpdateDbContext _dbContext;

        public UpdateOperationTests()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestUpdateAutoMapperProfile>()).CreateMapper();
            _currentUtcTimeProviderMock = new Mock<ICurrentUtcTimeProvider>();
            _updateOperation = new UpdateOperation(
                _mapper,
                _currentUtcTimeProviderMock.Object);

            var options = new DbContextOptionsBuilder<TestUpdateDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new TestUpdateDbContext(options);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntityWithCurrentUtcTimeAndUserId()
        {
            // Arrange
            var model = new TestUpdateModel { Name = "Updated Test" };
            var userId = "test-user-id";
            var id = Guid.NewGuid();
            var entity = new TestUpdateEntity
            {
                Id = id,
                UserId = userId,
                Name = "Original Test"
            };

            _currentUtcTimeProviderMock.Setup(x => x.GetCurrentUtcTime()).Returns(DateTimeOffset.UtcNow);

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            // Act
            await _updateOperation.UpdateAsync(_dbContext, (id, userId) => _dbContext.Set<TestUpdateEntity>().FindAsync(id).AsTask(), id, model, userId);

            // Assert
            var actualEntity = await _dbContext.Set<TestUpdateEntity>().FindAsync(id);
            actualEntity.Should().NotBeNull();
            actualEntity?.Name.Should().Be("Updated Test");
            actualEntity?.UpdatedBy.Should().Be(userId);
            actualEntity?.UpdatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(5));
        }

        internal class TestUpdateDbContext(DbContextOptions options) : DbContext(options)
        {
            public DbSet<TestUpdateEntity> TestUpdateEntities { get; set; }
        }

        internal class TestUpdateAutoMapperProfile : Profile
        {
            public TestUpdateAutoMapperProfile()
            {
                CreateMap<TestUpdateModel, TestUpdateEntity>();
            }
        }

        internal class TestUpdateEntity : EntityWithUserBase, IUpdateHistory
        {
            public required string Name { get; set; }
            public DateTimeOffset? UpdatedAt { get; set; }
            public string? UpdatedBy { get; set; }
        }

        internal class TestUpdateModel : ModelBase
        {
            public required string Name { get; set; }
        }
    }
}
