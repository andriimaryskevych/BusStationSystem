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
    public class LogRepository : IRepository<Log>
    {
        private ApplicationContext database;

        public LogRepository(ApplicationContext conBus)
        {
            this.database = conBus;
        }

        public IEnumerable<Log> GetAll()
        {
            return database.Logs;
        }

        public Log Get(string id)
        {
            return database.Logs.Find(id);
        }


        public IEnumerable<Log> Find(Func<Log, Boolean> predicate)
        {
            return database.Logs.Where(predicate);
        }

        public void Create(Log item)
        {
            database.Logs.Add(item);
        }

        public void Update(Log item)
        {
            database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Log item)
        {
            database.Logs.Remove(item);
        }

        public void Delete(string id)
        {
            Log item = database.Logs.Find(id);
            if (item != null)
                database.Logs.Remove(item);
        }
    }
}
