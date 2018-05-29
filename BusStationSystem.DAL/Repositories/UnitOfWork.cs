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
        private ApplicationIdentityContext identityDatabase;

        private RouteRepository routeRepository;
        private BusRepository busRepository;
        private TicketHistoryRepositoty tickethistoryRepositoty;
        private TicketsRepository ticketsRepository;
        private LogRepository logRepository;
        private UserRepository userRepository;
        private ClientRepository clientRepository;

        public UnitOfWork(ApplicationContext applicationContext, ApplicationIdentityContext applicationIdentityContext)
        {
            database = applicationContext;
            identityDatabase = applicationIdentityContext;
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(identityDatabase);
                return userRepository;
            }
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

        public IRepository<Log> Logs
        {
            get
            {
                if (logRepository == null)
                    logRepository = new LogRepository(database);
                return logRepository;
            }
        }

        public IRepository<Tickets> Tickets
        {
            get
            {
                if (ticketsRepository == null)
                    ticketsRepository = new TicketsRepository(database);
                return ticketsRepository;
            }
        }

        public IRepository<TicketHistory> TicketHistories
        {
            get
            {
                if (tickethistoryRepositoty== null)
                    tickethistoryRepositoty = new TicketHistoryRepositoty(database);
                return tickethistoryRepositoty;
            }
        }

        public IRepository<ClientProfile> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(database);
                return clientRepository;
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
