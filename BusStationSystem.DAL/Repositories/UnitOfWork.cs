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

        private AdviceRepository adviceRepository;
        private BusRepository busRepository;
        private ClientRepository clientRepository;
        private RouteRepository routeRepository;
        private StationRepository stationRepository;
        private TicketsRepository ticketsRepository;
        private TicketHistoryRepositoty tickethistoryRepositoty;

        private UserRepository userRepository;

        public UnitOfWork(ApplicationContext applicationContext, ApplicationIdentityContext applicationIdentityContext)
        {
            database = applicationContext;
            identityDatabase = applicationIdentityContext;
        }

        public IRepository<Advice> Advices
        {
            get
            {
                if (adviceRepository == null)
                    adviceRepository = new AdviceRepository(database);
                return adviceRepository;
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
        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(database);
                return clientRepository;
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
        public IRepository<Station> Stations
        {
            get
            {
                if (stationRepository == null)
                    stationRepository = new StationRepository(database);
                return stationRepository;
            }
        }
        public IRepository<Ticket> Tickets
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

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(identityDatabase);
                return userRepository;
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
