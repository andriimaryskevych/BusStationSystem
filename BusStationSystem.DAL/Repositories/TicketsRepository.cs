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
    public class TicketsRepository : IRepository<Tickets>
    {
        private ApplicationContext database;

        public TicketsRepository(ApplicationContext conBus)
        {
            this.database = conBus;
        }

        public IEnumerable<Tickets> GetAll()
        {
            return database.Tickets;
        }

        public Tickets Get(string id)
        {
            return database.Tickets.Find(id);
        }


        public IEnumerable<Tickets> Find(Func<Tickets, Boolean> predicate)
        {
            return database.Tickets.Where(predicate);
        }

        public void Create(Tickets item)
        {
            database.Tickets.Add(item);
        }

        public void Update(Tickets item)
        {
            database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Tickets item)
        {
            database.Tickets.Remove(item);
        }

        public void Delete(string id)
        {
            Tickets item = database.Tickets.Find(id);
            if (item != null)
                database.Tickets.Remove(item);
        }
    }
}
