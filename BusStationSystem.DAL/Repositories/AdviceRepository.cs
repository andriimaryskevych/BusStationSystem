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
    public class AdviceRepository : IRepository<Advice>
    {
        private ApplicationContext database;

        public AdviceRepository(ApplicationContext conBus)
        {
            this.database = conBus;
        }

        public IEnumerable<Advice> GetAll()
        {
            return database.Advices;
        }

        public Advice Get(int id)
        {
            return database.Advices.Find(id);
        }


        public IEnumerable<Advice> Find(Func<Advice, Boolean> predicate)
        {
            return database.Advices.Where(predicate);
        }

        public void Create(Advice item)
        {
            database.Advices.Add(item);
        }

        public void Update(Advice item)
        {
            database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Advice item)
        {
            database.Advices.Remove(item);
        }

        public void Delete(int id)
        {
            Advice item = database.Advices.Find(id);
            if (item != null)
                database.Advices.Remove(item);
        }
    }

}

