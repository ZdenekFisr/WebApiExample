using Application.Helpers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.IntegrationTests.FeaturesTests.RailVehicles
{
    [Collection("Database")]
    public class TrainIntegrationTestsBase(
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

        protected async Task<Train?> FindTrainByNameAsync(string trainName, string userId)
            => await _dbContext.Trains
                .Include(t => t.TrainVehicles)
                .FirstOrDefaultAsync(t => t.Name == trainName && t.UserId == userId);

        protected async Task<(Guid[] vehicleIds, Guid[] trainIds, string user1Id, string user2Id)> AddTestEntitiesToDbAsync()
        {
            Guid[] elTypeIds = GuidHelpers.GenerateRandomGuids(4).ToArray();
            Guid[] vehicleIds = GuidHelpers.GenerateRandomGuids(8).ToArray();
            Guid[] trainIds = GuidHelpers.GenerateRandomGuids(5).ToArray();
            string user1Id = Guid.NewGuid().ToString();
            string user2Id = Guid.NewGuid().ToString();

            ElectrificationType elType = _entityProvider.GetElectrificationTypes(elTypeIds, user1Id, user2Id).First();
            (RailVehicle[] testVehicles, Train[] testTrains) = _entityProvider.GetTestVehiclesAndTrains(vehicleIds, [elType.Id], trainIds, user1Id, user2Id);

            await _dbContext.Users.AddRangeAsync(_entityProvider.GetUsers(user1Id, user2Id));
            await _dbContext.ElectrificationTypes.AddAsync(elType);
            await _dbContext.RailVehicles.AddRangeAsync(testVehicles);
            await _dbContext.Trains.AddRangeAsync(testTrains);

            await _dbContext.SaveChangesAsync();

            return (vehicleIds, trainIds, user1Id, user2Id);
        }
    }
}
