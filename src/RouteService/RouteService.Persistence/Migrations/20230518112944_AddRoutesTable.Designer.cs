﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RouteService.Persistence;

#nullable disable

namespace RouteService.Persistence.Migrations
{
    [DbContext(typeof(RouteServiceDbContext))]
    [Migration("20230518112944_AddRoutesTable")]
    partial class AddRoutesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RouteService.Domain.Entities.Ride", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<string>("RouteId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RouteInfoId")
                        .HasColumnType("int");

                    b.Property<int>("SeatsAvailable")
                        .HasColumnType("int");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RouteInfoId");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("RouteService.Domain.Entities.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ExtraInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("RouteService.Domain.Entities.Ride", b =>
                {
                    b.HasOne("RouteService.Domain.Entities.Route", "RouteInfo")
                        .WithMany()
                        .HasForeignKey("RouteInfoId");

                    b.Navigation("RouteInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
