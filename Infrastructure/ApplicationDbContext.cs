using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
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
                .HasMany(e => e.TractionDiagram)
                .WithOne()
                .HasForeignKey(e => e.RailVehicleId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RailVehicle>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.UserId);
            builder.Entity<RailVehicle>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.CreatedBy);
            builder.Entity<RailVehicle>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy);
            builder.Entity<RailVehicle>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.DeletedBy);
        }
    }
}
