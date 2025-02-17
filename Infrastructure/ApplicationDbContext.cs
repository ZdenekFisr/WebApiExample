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

        public DbSet<ElectrificationType> ElectrificationTypes { get; set; }

        public DbSet<RailVehicle> RailVehicles { get; set; }

        public DbSet<Train> Trains { get; set; }

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
                .HasForeignKey(e => e.CreatedBy);
            builder.Entity<RailVehicle>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy);
            builder.Entity<RailVehicle>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.DeletedBy);
            builder.Entity<RailVehicle>()
                .HasMany(v => v.TractionSystems)
                .WithOne()
                .HasForeignKey(vts => vts.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<VehicleTractionSystem>()
                .HasOne<ElectrificationType>()
                .WithMany()
                .HasForeignKey(vts => vts.ElectrificationTypeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<VehicleTractionSystem>()
                .HasMany(v => v.TractionDiagram)
                .WithOne()
                .HasForeignKey(tdp => tdp.VehicleTractionSystemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Train>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.UserId);
            builder.Entity<Train>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.CreatedBy);
            builder.Entity<Train>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy);
            builder.Entity<Train>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.DeletedBy);
            builder.Entity<Train>()
                .HasMany(t => t.TrainVehicles)
                .WithOne()
                .HasForeignKey(tv => tv.TrainId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TrainVehicle>()
                .HasOne<RailVehicle>()
                .WithMany()
                .HasForeignKey(tv => tv.VehicleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ElectrificationType>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.UserId);
            builder.Entity<ElectrificationType>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.CreatedBy);
            builder.Entity<ElectrificationType>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy);
            builder.Entity<ElectrificationType>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(e => e.DeletedBy);
        }
    }
}
