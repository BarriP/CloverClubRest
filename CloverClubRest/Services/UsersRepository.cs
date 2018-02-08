using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloverClubRest.Models;
using Microsoft.EntityFrameworkCore;

namespace CloverClubRest.Services
{
    public class UsersRepository : IUsersRepository, IDisposable
    {
        private readonly UsersContext _context;

        public UsersRepository(UsersContext ctx) => _context = ctx;

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(long userId)
        {
            return _context.Users.Find(userId);
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
        }

        public void DeleteUser(long userId)
        {
            var user = GetUserById(userId);
            _context.Users.Remove(user);
;        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
