﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiExample;

#nullable disable

namespace WebApiExample.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApiExample.Features.FilmDatabase.Film", b =>
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

            modelBuilder.Entity("WebApiExample.Features.NumberInWords.CurrencyCzechName", b =>
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
#pragma warning restore 612, 618
        }
    }
}
