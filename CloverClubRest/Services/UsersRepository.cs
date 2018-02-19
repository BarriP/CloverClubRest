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

        public IEnumerable<User> GetUsers() => _context.User.Include(u => u.CoctelesFav).Include(u => u.IngredientesFav).ToList();

        public User FindByEmail(string email) => _context.User.Include(u => u.CoctelesFav).Include(u => u.IngredientesFav).FirstOrDefault(u => u.Email.Equals(email));

        public User GetUserById(int userId) => _context.User.Include(u => u.CoctelesFav).Include(u => u.IngredientesFav).FirstOrDefault(u => u.Id == userId);

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

        public void Save() => _context.SaveChanges();

        public bool AddCoctelFav(User user, int id)
        {
            if (user.CoctelesFavList.Contains(id))
                return false;

            var fav = new CoctelFav
            {
                Userid = user.Id,
                Coctelid = id
            };
            _context.CoctelFav.Add(fav);

            return true;
        }

        public bool AddIngredienteFav(User user, string ing)
        {
            if (user.IngredientesFavList.Contains(ing))
                return false;

            var fav = new IngredienteFav
            {
                Userid = user.Id,
                Ingrediente = ing
            };
            _context.IngredienteFav.Add(fav);

            return true;
        }

        public bool RemoveCoctelFav(User user, int id)
        {
            if (!user.CoctelesFavList.Contains(id))
                return false;

            var cf = _context.CoctelFav.Find(user.Id, id);
            _context.CoctelFav.Remove(cf);
            return true;
        }

        public bool RemoveIngredienteFav(User user, string ing)
        {
            if (!user.IngredientesFavList.Contains(ing))
                return false;

            var ifav = _context.IngredienteFav.Find(user.Id, ing);
            _context.IngredienteFav.Remove(ifav);
            return true;
        }

        #region Dispose
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
        #endregion
    }
}
