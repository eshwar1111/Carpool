﻿// <auto-generated />
using System;
using Carpool.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230217120359_up1")]
    partial class up1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Database.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Database.Ride", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookedById")
                        .HasColumnType("int");

                    b.Property<bool>("IsBooked")
                        .HasColumnType("bit");

                    b.Property<int>("OfferedById")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("RideDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RideTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("Database.Stop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int?>("RideId")
                        .HasColumnType("int");

                    b.Property<int>("SeatsAtStop")
                        .HasColumnType("int");

                    b.Property<int>("StopIndex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RideId");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("Database.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Database.Stop", b =>
                {
                    b.HasOne("Database.Ride", null)
                        .WithMany("Stops")
                        .HasForeignKey("RideId");
                });

            modelBuilder.Entity("Database.Ride", b =>
                {
                    b.Navigation("Stops");
                });
#pragma warning restore 612, 618
        }
    }
}