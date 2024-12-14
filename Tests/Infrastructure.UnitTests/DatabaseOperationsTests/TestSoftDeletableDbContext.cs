using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitTests.DatabaseOperationsTests
{
    public class TestSoftDeletableDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<TestSoftDeletableEntity> TestSoftDeletableEntities { get; set; }
    }
}
