using BusStationSystem.DAL.EF;
using BusStationSystem.DAL.Entities;
using BusStationSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BusStationSystem.DAL.Repositories
{
    public class StationRepository : IRepository<Station>
    {
        private ApplicationContext database;

        public StationRepository(ApplicationContext conBus)
        {
            this.database = conBus;
        }

        public IEnumerable<Station> GetAll(Expression<Func<Station, object>>[] paths = null)
        {
            return database.Stations;
        }

        public Station Get(int id)
        {
            return database.Stations.Find(id);
        }


        public IEnumerable<Station> Find(Func<Station, Boolean> predicate)
        {
            return database.Stations.Where(predicate);
        }

        public void Create(Station item)
        {
            database.Stations.Add(item);
        }

        public void Update(Station item)
        {
            database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Station item)
        {
            database.Stations.Remove(item);
        }

        public void Delete(int id)
        {
            Station item = database.Stations.Find(id);
            if (item != null)
                database.Stations.Remove(item);
        }
    }

}

