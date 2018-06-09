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
    [Migration("20180609152943_Initial2")]
    partial class Initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Advice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdviceText");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Advices");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Bus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Make");

                    b.Property<string>("Model");

                    b.Property<DateTime>("ProductionYear");

                    b.HasKey("Id");

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adress");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ArrivalDate");

                    b.Property<int>("ArrivalId");

                    b.Property<int>("BusId");

                    b.Property<int>("DepartureId");

                    b.Property<DateTime>("DetartureDate");

                    b.Property<int>("Price");

                    b.HasKey("Id");

                    b.HasIndex("ArrivalId");

                    b.HasIndex("BusId");

                    b.HasIndex("DepartureId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("StationName");

                    b.HasKey("Id");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<int>("RouteiD");

                    b.Property<DateTime>("SaleDate");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("RouteiD");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.TicketHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Employee");

                    b.Property<int>("Ticket");

                    b.HasKey("Id");

                    b.ToTable("TicketHistories");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Advice", b =>
                {
                    b.HasOne("BusStationSystem.DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Route", b =>
                {
                    b.HasOne("BusStationSystem.DAL.Entities.Station", "Arrival")
                        .WithMany()
                        .HasForeignKey("ArrivalId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusStationSystem.DAL.Entities.Bus", "Bus")
                        .WithMany()
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusStationSystem.DAL.Entities.Station", "Departure")
                        .WithMany()
                        .HasForeignKey("DepartureId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BusStationSystem.DAL.Entities.Ticket", b =>
                {
                    b.HasOne("BusStationSystem.DAL.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusStationSystem.DAL.Entities.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteiD")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
