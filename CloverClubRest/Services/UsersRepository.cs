using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloverClubRest.Models;
using Microsoft.EntityFrameworkCore;

namespace CloverClubRest.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersContext _context;

        public UsersRepository(UsersContext ctx) => _context = ctx;

        public IEnumerable<User> GetUsers() => _context.User.ToList();

        public User GetUserById(int userId) => _context.User.Find(userId);

        public User InsertUser(User user)
        {
            var newUser = _context.User.Add(user);
            return newUser.Entity;
        }

        public bool DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            if (user == null)
                return false;
            _context.User.Remove(user);
            return true;
;        }

        public User UpdateUser(User user)
        {
            if (GetUserById(user.Id) == null)
                return null;
            _context.Entry(user).State = EntityState.Modified;
            return user;
        }

        public User FindByEmail(string email) => _context.User.FirstOrDefault(u => u.Email.Equals(email));

        public void Save() => _context.SaveChanges();

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
