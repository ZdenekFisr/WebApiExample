using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Respawn;
using System.Data.Common;

namespace Infrastructure.IntegrationTests
{
    public class DatabaseFixture : IAsyncLifetime
    {
        public ApplicationDbContext Context { get; private set; }

        private Respawner _respawner;
        private DbConnection _connection;

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .EnableSensitiveDataLogging()
                .UseSqlServer(configuration.GetConnectionString("TestDatabase"), builder => builder.MigrationsAssembly("Infrastructure"))
                .Options;

            Context = new ApplicationDbContext(options);
            await Context.Database.EnsureDeletedAsync();
            await Context.Database.MigrateAsync();

            var respawnerOptions = new RespawnerOptions
            {
                SchemasToInclude =
                [
                    "dbo"
                ],
                DbAdapter = DbAdapter.SqlServer
            };

            _connection = Context.Database.GetDbConnection();
            await _connection.OpenAsync();

            _respawner = await Respawner.CreateAsync(_connection, respawnerOptions);
        }

        public Task ResetDatabase()
        {
            return _respawner.ResetAsync(_connection);
        }

        public async Task DisposeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.DisposeAsync();
            await _connection.CloseAsync();
        }
    }
}
