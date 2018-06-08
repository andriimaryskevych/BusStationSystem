using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using BusStationSystem.DAL.Entities;

namespace BusStationSystem.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Advice> Advices { get; }
        IRepository<Bus> Buses { get; }
        IRepository<Client> Clients { get; }
        IRepository<Route> Routes { get; }
        IRepository<Station> Stations { get; }
        IRepository<Ticket> Tickets { get; }
        IRepository<TicketHistory> TicketHistories { get; }

        IRepository<User> Users { get; }

        void Save();
    }
}
