﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250223141256_CustomUser")]
    partial class CustomUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.CurrencyCzechName", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FiveOrMoreSubunits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FiveOrMoreUnits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OneSubunit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OneUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubunitGrammaticalGender")
                        .HasColumnType("int");

                    b.Property<string>("TwoToFourSubunits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TwoToFourUnits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitGrammaticalGender")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CurrencyCzechNames");
                });

            modelBuilder.Entity("Domain.Entities.ElectrificationType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Voltage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DeletedBy");

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UserId");

                    b.ToTable("ElectrificationTypes");
                });

            modelBuilder.Entity("Domain.Entities.Film", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("LengthInMinutes")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte>("Rating")
                        .HasColumnType("tinyint");

                    b.Property<short>("YearOfRelease")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("Domain.Entities.RailVehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<double>("EquivalentRotatingWeight")
                        .HasColumnType("float");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<short>("MaxSpeed")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<double>("ResistanceConstant")
                        .HasColumnType("float");

                    b.Property<double>("ResistanceLinear")
                        .HasColumnType("float");

                    b.Property<double>("ResistanceQuadratic")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<byte>("Wheelsets")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DeletedBy");

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UserId");

                    b.ToTable("RailVehicles");
                });

            modelBuilder.Entity("Domain.Entities.TractionDiagramPoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("PullForce")
                        .HasColumnType("float");

                    b.Property<double>("Speed")
                        .HasColumnType("float");

                    b.Property<Guid>("VehicleTractionSystemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VehicleTractionSystemId");

                    b.ToTable("TractionDiagramPoints");
                });

            modelBuilder.Entity("Domain.Entities.Train", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<short>("MaxPullForce")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DeletedBy");

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UserId");

                    b.ToTable("Trains");
                });

            modelBuilder.Entity("Domain.Entities.TrainVehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<short>("Position")
                        .HasColumnType("smallint");

                    b.Property<Guid>("TrainId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("VehicleCount")
                        .HasColumnType("smallint");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TrainId");

                    b.HasIndex("VehicleId");

                    b.ToTable("TrainVehicles");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.VehicleTractionSystem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("DrivingWheelsets")
                        .HasColumnType("tinyint");

                    b.Property<double>("Efficiency")
                        .HasColumnType("float");

                    b.Property<Guid?>("ElectrificationTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("MaxPullForce")
                        .HasColumnType("smallint");

                    b.Property<short>("MaxSpeed")
                        .HasColumnType("smallint");

                    b.Property<short>("Performance")
                        .HasColumnType("smallint");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("VoltageCoefficient")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ElectrificationTypeId");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleTractionSystems");
                });

            modelBuilder.Entity("Domain.Entities.ElectrificationType", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("DeletedBy");

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.RailVehicle", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("DeletedBy");

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.TractionDiagramPoint", b =>
                {
                    b.HasOne("Domain.Entities.VehicleTractionSystem", null)
                        .WithMany("TractionDiagram")
                        .HasForeignKey("VehicleTractionSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Train", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("DeletedBy");

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.TrainVehicle", b =>
                {
                    b.HasOne("Domain.Entities.Train", null)
                        .WithMany("TrainVehicles")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.RailVehicle", null)
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.VehicleTractionSystem", b =>
                {
                    b.HasOne("Domain.Entities.ElectrificationType", null)
                        .WithMany()
                        .HasForeignKey("ElectrificationTypeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.RailVehicle", null)
                        .WithMany("TractionSystems")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.RailVehicle", b =>
                {
                    b.Navigation("TractionSystems");
                });

            modelBuilder.Entity("Domain.Entities.Train", b =>
                {
                    b.Navigation("TrainVehicles");
                });

            modelBuilder.Entity("Domain.Entities.VehicleTractionSystem", b =>
                {
                    b.Navigation("TractionDiagram");
                });
#pragma warning restore 612, 618
        }
    }
}
