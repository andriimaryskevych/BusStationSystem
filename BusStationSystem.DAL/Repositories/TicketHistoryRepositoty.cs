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
    public class TicketHistoryRepositoty : IRepository<TicketHistory>
    {
        private ApplicationContext database;

        public TicketHistoryRepositoty(ApplicationContext conBus)
        {
            this.database = conBus;
        }

        public IEnumerable<TicketHistory> GetAll()
        {
            return database.TicketHistories;
        }

        public TicketHistory Get(string id)
        {
            return database.TicketHistories.Find(id);
        }


        public IEnumerable<TicketHistory> Find(Func<TicketHistory, Boolean> predicate)
        {
            return database.TicketHistories.Where(predicate);
        }

        public void Create(TicketHistory item)
        {
            database.TicketHistories.Add(item);
        }

        public void Update(TicketHistory item)
        {
            database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(TicketHistory item)
        {
            database.TicketHistories.Remove(item);
        }

        public void Delete(string id)
        {
            TicketHistory item = database.TicketHistories.Find(id);
            if (item != null)
                database.TicketHistories.Remove(item);
        }
    }
}
