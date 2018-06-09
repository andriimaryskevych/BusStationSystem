using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Linq;
using BusStationSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusStationSystem.DAL.EF
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            
        }

        public DbSet<Advice> Advices { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketHistory> TicketHistories { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Route>()
        //        .HasOne(u => u.Arrival)
        //        .WithMany(u => u.Arrivals)
        //        .HasForeignKey(u => u.ArrivalId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    builder.Entity<Route>()
        //        .HasOne(u => u.Departure)
        //        .WithMany(u => u.Departures)
        //        .HasForeignKey(u => u.DepartureId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    //builder.Entity<Station>()
        //    //    .HasMany(u => u.Arrivals)
        //    //    .WithOne(u => u.Arrival)
        //    //    .HasForeignKey(u => u.ArrivalId)
        //    //    .OnDelete(DeleteBehavior.Cascade);

        //    //builder.Entity<Station>()
        //    //    .HasMany(u => u.Departures)
        //    //    .WithOne(u => u.Departure)
        //    //    .HasForeignKey(u => u.DepartureId)
        //    //    .OnDelete(DeleteBehavior.Restrict);
        //}
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelbuilder.Entity<Station>()
                .HasMany(c => c.Departures)
                .WithOne(e => e.Departure)
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<Station>()
                .HasMany(c => c.Arrivals)
                .WithOne(e => e.Arrival)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelbuilder);
        }
    }
}
