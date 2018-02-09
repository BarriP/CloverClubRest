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
            return _context.User.ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.User.Find(userId);
        }

        public User InsertUser(User user)
        {
            var newUser = _context.User.Add(user);
            return newUser.Entity;
        }

        public void DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            _context.User.Remove(user);
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
