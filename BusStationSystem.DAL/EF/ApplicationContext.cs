using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
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

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<TicketHistory> TicketHistories { get; set; }
    }
}
