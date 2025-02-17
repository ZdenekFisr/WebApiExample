using Application.Common;
using Application.Services;
using AutoMapper;
using Domain.Common;
using FluentAssertions;
using Infrastructure.DatabaseOperations.Insert;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.UnitTests.DatabaseOperationsTests
{
    public class InsertOperationTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICurrentUtcTimeProvider> _currentUtcTimeProviderMock;
        private readonly InsertOperation _insertOperation;
        private readonly TestInsertDbContext _dbContext;

        public InsertOperationTests()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestInsertAutoMapperProfile>()).CreateMapper();
            _currentUtcTimeProviderMock = new Mock<ICurrentUtcTimeProvider>();
            _insertOperation = new InsertOperation(
                _mapper,
                _currentUtcTimeProviderMock.Object);

            var options = new DbContextOptionsBuilder<TestInsertDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new TestInsertDbContext(options);

        }

        [Fact]
        public async Task InsertAsync_Should_MapModelToEntity_And_SaveToDatabase()
        {
            // Arrange
            var userId = "test-user-id";
            var model = new TestInsertModel
            {
                Name = "Test Model"
            };

            _currentUtcTimeProviderMock.Setup(p => p.GetCurrentUtcTime()).Returns(DateTimeOffset.UtcNow);

            // Act
            await _insertOperation.InsertAsync<TestInsertEntity, TestInsertModel>(_dbContext, model, userId);

            // Assert
            var savedEntity = await _dbContext.Set<TestInsertEntity>().FirstOrDefaultAsync(e => e.Name == model.Name);
            savedEntity.Should().NotBeNull();
            savedEntity?.Name.Should().Be(model.Name);
            savedEntity?.UserId.Should().Be(userId);
            savedEntity?.CreatedBy.Should().Be(userId);
            savedEntity?.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(5));
        }

        private class TestInsertDbContext(DbContextOptions options) : DbContext(options)
        {
            public DbSet<TestInsertEntity> TestInsertEntities { get; set; }
        }

        private class TestInsertAutoMapperProfile : Profile
        {
            public TestInsertAutoMapperProfile()
            {
                CreateMap<TestInsertModel, TestInsertEntity>();
            }
        }

        private class TestInsertEntity : EntityWithUserBase, ICreateHistory
        {
            public required string Name { get; set; }
            public DateTimeOffset CreatedAt { get; set; }
            public string? CreatedBy { get; set; }
        }

        private class TestInsertModel : ModelBase
        {
            public required string Name { get; set; }
        }
    }
}
