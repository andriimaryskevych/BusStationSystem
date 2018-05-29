﻿// <auto-generated />
using BusStationSystem.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BusStationSystem.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Bus", b =>
                {
                    b.Property<string>("BusNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PlaceCount");

                    b.Property<string>("Type");

                    b.HasKey("BusNumber");

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.ClientProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adress");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("EmploeeId");

                    b.Property<string>("Hours");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Route", b =>
                {
                    b.Property<string>("RouteNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ArrivalDate");

                    b.Property<string>("BusId");

                    b.Property<string>("Destination");

                    b.Property<DateTime>("DetartureDate");

                    b.Property<string>("OccupiedPlaceCount");

                    b.Property<string>("RouteType");

                    b.HasKey("RouteNumber");

                    b.HasIndex("BusId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.TicketHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmploeeId");

                    b.Property<string>("TicketId");

                    b.HasKey("Id");

                    b.ToTable("TicketHistories");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Tickets", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<int>("ClientId");

                    b.Property<int>("PlaceNumber");

                    b.Property<double>("Price");

                    b.Property<string>("RouteNumber");

                    b.Property<DateTime>("SaleDate");

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Route", b =>
                {
                    b.HasOne("BusStationSystem.DAL.Entities.Bus", "Bus")
                        .WithMany()
                        .HasForeignKey("BusId");
                });
#pragma warning restore 612, 618
        }
    }
}
