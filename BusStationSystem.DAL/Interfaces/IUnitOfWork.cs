using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using BusStationSystem.DAL.Entities;

namespace BusStationSystem.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Route> Routes { get; }
        IRepository<Bus> Buses { get; }

        void Save();
    }
}
