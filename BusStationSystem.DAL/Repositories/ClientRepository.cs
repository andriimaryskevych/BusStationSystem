﻿using BusStationSystem.DAL.EF;
using BusStationSystem.DAL.Entities;
using BusStationSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusStationSystem.DAL.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private ApplicationContext database;

        public ClientRepository(ApplicationContext conBus)
        {
            this.database = conBus;
        }

        public IEnumerable<Client> GetAll()
        {
            return database.Clients;
        }

        public Client Get(string id)
        {
            return database.Clients.Find(id);
        }


        public IEnumerable<Client> Find(Func<Client, Boolean> predicate)
        {
            return database.Clients.Where(predicate);
        }

        public void Create(Client item)
        {
            database.Clients.Add(item);
        }

        public void Update(Client item)
        {
            database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Client item)
        {
            database.Clients.Remove(item);
        }

        public void Delete(string id)
        {
            Client item = database.Clients.Find(id);
            if (item != null)
                database.Clients.Remove(item);
        }
    }

}

