using Microsoft.EntityFrameworkCore;
using WebApiExample.Features.FilmDatabase;
using WebApiExample.Features.NumberInWords;

namespace WebApiExample
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<CurrencyCzechName> CurrencyCzechNames { get; set; }

        public DbSet<Film> Films { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
