using Microsoft.EntityFrameworkCore;
using WebApiExample.Features.FilmDatabase;
using WebApiExample.Features.NumberInWords;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApiExample
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<CurrencyCzechName> CurrencyCzechNames { get; set; }

        public DbSet<Film> Films { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
