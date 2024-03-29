﻿using System;
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
        bool DeleteUser(int userId);
        User UpdateUser(User user);
        User FindByEmail(string email);
        void Save();

        bool AddCoctelFav(User user, int id);
        bool AddIngredienteFav(User user, string ing);
        bool RemoveCoctelFav(User user, int id);
        bool RemoveIngredienteFav(User user, string ing);
    }
}
