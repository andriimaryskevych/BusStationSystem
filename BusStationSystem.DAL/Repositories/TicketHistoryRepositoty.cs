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
    public class TicketHistoryRepositoty : IRepository<TicketHistory>
    {
        private ApplicationContext database;

        public TicketHistoryRepositoty(ApplicationContext conBus)
        {
            this.database = conBus;
        }

        public IEnumerable<TicketHistory> GetAll(Expression<Func<TicketHistory, object>>[] paths = null)
        {
            return database.TicketHistories
                .Include(a => a.Ticket)
                    .ThenInclude(a => a.Route)
                        .ThenInclude(a => a.Arrival)
                 .Include(a => a.Ticket)
                    .ThenInclude(a => a.Route)
                        .ThenInclude(a => a.Departure)
                 .Include(a => a.Ticket)
                    .ThenInclude(a => a.Client);
        }

        public TicketHistory Get(int id)
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

        public void Delete(int id)
        {
            TicketHistory item = database.TicketHistories.Find(id);
            if (item != null)
                database.TicketHistories.Remove(item);
        }
    }
}
