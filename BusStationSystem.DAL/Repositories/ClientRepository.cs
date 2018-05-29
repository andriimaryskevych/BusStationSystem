using BusStationSystem.DAL.EF;
using BusStationSystem.DAL.Entities;
using BusStationSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusStationSystem.DAL.Repositories
{
    public class ClientRepository : IRepository<ClientProfile>
    {
        private ApplicationContext database;

        public ClientRepository(ApplicationContext conBus)
        {
            this.database = conBus;
        }

        public IEnumerable<ClientProfile> GetAll()
        {
            return database.Clients;
        }

        public ClientProfile Get(string id)
        {
            return database.Clients.Find(id);
        }


        public IEnumerable<ClientProfile> Find(Func<ClientProfile, Boolean> predicate)
        {
            return database.Clients.Where(predicate);
        }

        public void Create(ClientProfile item)
        {
            database.Clients.Add(item);
        }

        public void Update(ClientProfile item)
        {
            database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(ClientProfile item)
        {
            database.Clients.Remove(item);
        }

        public void Delete(string id)
        {
            ClientProfile item = database.Clients.Find(id);
            if (item != null)
                database.Clients.Remove(item);
        }
    }

}

