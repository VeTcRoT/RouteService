﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RouteService.Persistence;

#nullable disable

namespace RouteService.Persistence.Migrations
{
    [DbContext(typeof(RouteServiceDbContext))]
    partial class RouteServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("RouteService.Domain.Entities.Ride", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RouteId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("RouteInfoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SeatsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RouteInfoId");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("RouteService.Domain.Entities.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExtraInfo")
                        .IsRequired()
                        .HasColumnType("TEXT");

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
