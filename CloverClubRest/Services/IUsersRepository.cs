using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloverClubRest.Models;

namespace CloverClubRest.Services
{
    public interface IUsersRepository : IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserById(long userId);
        void InsertUser(User user);
        void DeleteUser(long userId);
        void UpdateUser(User user);
        void Save();
    }
}
