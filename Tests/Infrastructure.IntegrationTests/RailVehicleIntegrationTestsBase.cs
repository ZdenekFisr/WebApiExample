using Application.Helpers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.IntegrationTests
{
    [Collection("Database")]
    public class RailVehicleIntegrationTestsBase(
        DatabaseFixture databaseFixture)
        : IAsyncLifetime
    {
        private readonly Func<Task> _resetDatabase = databaseFixture.ResetDatabase;

        protected readonly ApplicationDbContext _dbContext = databaseFixture.Context;
        protected readonly EntityProvider _entityProvider = new();

        protected readonly TimeSpan offset = new(0);
        protected readonly TimeSpan timeDelta = TimeSpan.FromSeconds(5);

        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync() => await _resetDatabase();

        protected async Task<RailVehicle?> FindVehicleByNameAsync(string vehicleName, string userId)
            => await _dbContext.RailVehicles
                .Include(v => v.TractionDiagram)
                .FirstOrDefaultAsync(v => v.Name == vehicleName && v.UserId == userId);

        protected async Task<(Guid[] vehicleIds, string user1Id, string user2Id)> AddTestEntitiesToDbAsync()
        {
            Guid[] vehicleIds = GuidHelpers.GenerateRandomGuids(10).ToArray();
            string user1Id = Guid.NewGuid().ToString();
            string user2Id = Guid.NewGuid().ToString();

            RailVehicle[] testVehicles = _entityProvider.GetTestVehicles(vehicleIds, user1Id, user2Id);
            await _dbContext.Users.AddRangeAsync([new() { Id = user1Id }, new() { Id = user2Id }]);
            await _dbContext.RailVehicles.AddRangeAsync(testVehicles);

            await _dbContext.SaveChangesAsync();

            return (vehicleIds, user1Id, user2Id);
        }
    }
}
