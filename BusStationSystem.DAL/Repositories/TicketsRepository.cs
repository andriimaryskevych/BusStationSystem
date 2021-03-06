﻿using BusStationSystem.DAL.EF;
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
    public class TicketsRepository : IRepository<Ticket>
    {
        private ApplicationContext database;

        public TicketsRepository(ApplicationContext conBus)
        {
            this.database = conBus;
        }

        public IEnumerable<Ticket> GetAll(Expression<Func<Ticket, object>>[] paths = null)
        {
            return database.Tickets.Include(a => a.Client);
        }

        public Ticket Get(int id)
        {
            return database.Tickets.Find(id);
        }


        public IEnumerable<Ticket> Find(Func<Ticket, Boolean> predicate)
        {
            return database.Tickets.Where(predicate);
        }

        public void Create(Ticket item)
        {
            database.Tickets.Add(item);
        }

        public void Update(Ticket item)
        {
            database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Ticket item)
        {
            database.Tickets.Remove(item);
        }

        public void Delete(int id)
        {
            Ticket item = database.Tickets.Find(id);
            if (item != null)
                database.Tickets.Remove(item);
        }
    }
}
