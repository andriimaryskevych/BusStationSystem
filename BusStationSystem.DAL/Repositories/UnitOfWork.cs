using BusStationSystem.DAL.EF;
using BusStationSystem.DAL.Entities;
using BusStationSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusStationSystem.DAL.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationContext database;

        private RouteRepository routeRepository;
        private BusRepository busRepository;

        public UnitOfWork(ApplicationContext applicationContext)
        {
            database = applicationContext;
        }

        public IRepository<Route> Routes
        {
            get
            {
                if (routeRepository == null)
                    routeRepository = new RouteRepository(database);
                return routeRepository;
            }
        }

        public IRepository<Bus> Buses
        {
            get
            {
                if (busRepository == null)
                    busRepository = new BusRepository(database);
                return busRepository;
            }
        }


        public void Save()
        {
            database.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    database.Dispose();
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
