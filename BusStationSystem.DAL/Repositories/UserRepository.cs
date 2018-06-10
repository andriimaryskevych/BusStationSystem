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
    class UserRepository : IRepository<User>
    {
        private ApplicationIdentityContext database;

        public UserRepository(ApplicationIdentityContext conBus)
        {
            this.database = conBus;
        }

        public IEnumerable<User> GetAll(Expression<Func<User, object>>[] paths = null)
        {
            return database.Users;
        }

        public User Get(int id)
        {
            return database.Users.Find(id);
        }


        public IEnumerable<User> Find(Func<User, Boolean> predicate)
        {
            return database.Users.Where(predicate);
        }

        public void Create(User item)
        {
            database.Users.Add(item);
        }

        public void Update(User item)
        {
            database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(User item)
        {
            database.Users.Remove(item);
        }

        public void Delete(int id)
        {
            User item = database.Users.Find(id);
            if (item != null)
                database.Users.Remove(item);
        }
    }
}
