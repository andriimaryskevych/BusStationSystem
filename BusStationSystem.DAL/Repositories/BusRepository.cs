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
    public class BusRepository : IRepository<Bus>
    {
        private ApplicationContext database;

        public BusRepository(ApplicationContext conBus)
        {
            this.database = conBus;
        }

        public IEnumerable<Bus> GetAll()
        {
            return database.Buses;
        }

        public Bus Get(string id)
        {
            return database.Buses.Find(id);
        }


        public IEnumerable<Bus> Find(Func<Bus, Boolean> predicate)
        {
            return database.Buses.Where(predicate);
        }

        public void Create(Bus item)
        {
            database.Buses.Add(item);
        }

        public void Update(Bus item)
        {
            database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Bus item)
        {
            database.Buses.Remove(item);
        }

        public void Delete(string id)
        {
            Bus item = database.Buses.Find(id);
            if (item != null)
                database.Buses.Remove(item);
        }
    }

}

