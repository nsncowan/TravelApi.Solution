﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelApi.Models;

#nullable disable

namespace TravelApi.Migrations
{
    [DbContext(typeof(TravelApiContext))]
    [Migration("20230409215012_AddSeeding")]
    partial class AddSeeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TravelApi.Models.Destination", b =>
                {
                    b.Property<int>("DestinationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Review")
                        .HasColumnType("longtext");

                    b.HasKey("DestinationId");

                    b.ToTable("Destination");

                    b.HasData(
                        new
                        {
                            DestinationId = 1,
                            City = "Tokyo",
                            Country = "Japan",
                            Rating = 5,
                            Review = ""
                        },
                        new
                        {
                            DestinationId = 2,
                            City = "Mumbai",
                            Country = "India",
                            Rating = 5,
                            Review = ""
                        },
                        new
                        {
                            DestinationId = 3,
                            City = "Paris",
                            Country = "France",
                            Rating = 5,
                            Review = ""
                        },
                        new
                        {
                            DestinationId = 4,
                            City = "London",
                            Country = "England",
                            Rating = 5,
                            Review = ""
                        },
                        new
                        {
                            DestinationId = 5,
                            City = "Sydney",
                            Country = "Australia",
                            Rating = 5,
                            Review = ""
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
