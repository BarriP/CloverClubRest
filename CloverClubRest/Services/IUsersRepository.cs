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
        User GetUserById(int userId);
        User InsertUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void Save();
    }
}
