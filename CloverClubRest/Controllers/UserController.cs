using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CloverClubRest.Models;
using CloverClubRest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloverClubRest.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUsersRepository userRepository;

        public UserController(IUsersRepository repo)
        {
            this.userRepository = repo;
        }

        [HttpGet]
        [Authorize("User")]
        public User Get()
        {
            return GetUser(HttpContext.User);
        }

        // GET: api/Favorites/Coctel
        [HttpGet("/Coctel")]
        public IEnumerable<string> GetCoctels()
        {
            return null;
        }

        // POST api/Favorites/Coctel
        [HttpPost("/Coctel")]
        public void PostCoctel([FromBody]int value)
        {

        }

        // DELETE api/Favorites/Coctel/2
        [HttpDelete("/Coctel/{id}")]
        public void DeleteCoctel(int id)
        {
        }

        // GET: api/Favorites/Ingredient
        [HttpGet("/Ingredient")]
        public IEnumerable<string> GetIngredients()
        {
            return null;
        }

        // POST api/Favorites/Ingredient
        [HttpPost("/Ingredient")]
        public void PostIngredient([FromBody]int value)
        {

        }

        // DELETE api/Favorites/Ingredient/2
        [HttpDelete("/Ingredient/{id}")]
        public void DeleteIngredient(int id)
        {
        }
        private User GetUser(ClaimsPrincipal user)
        {
            string textId = user.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            if (Int32.TryParse(textId, out int id))
                return userRepository.GetUserById(id);
            else
                return null;
        }
    }
}
