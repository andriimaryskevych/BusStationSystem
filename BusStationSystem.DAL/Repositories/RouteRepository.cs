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
    public class RouteRepository : IRepository<Route>
    {
        private ApplicationContext database;

        public RouteRepository(ApplicationContext conRoute)
        {
            this.database = conRoute;
        }

        public IEnumerable<Route> GetAll()
        {
            return database.Routes;
        }

        public Route Get(int id)
        {
            return database.Routes.Find(id);
        }


        public IEnumerable<Route> Find(Func<Route, Boolean> predicate)
        {
            return database.Routes.Where(predicate);
        }

        public void Create(Route item)
        {
            database.Routes.Add(item);
        }

        public void Update(Route item)
        {
            database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Route item)
        {
            database.Routes.Remove(item);
        }

        public void Delete(int id)
        {
            Route item = database.Routes.Find(id);
            if (item != null)
                database.Routes.Remove(item);
        }
    }
}
