using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiExample.Features.AmountInWords;
using WebApiExample.Features.FilmDatabase;
using WebApiExample.Features.RailVehicles;

namespace WebApiExample
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<CurrencyCzechName> CurrencyCzechNames { get; set; }

        public DbSet<Film> Films { get; set; }

        public DbSet<RailVehicle> RailVehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RailVehicle>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.UserId);
            builder.Entity<RailVehicle>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.DeletedBy);
        }
    }
}
