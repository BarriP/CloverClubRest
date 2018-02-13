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

        public UserController(IUsersRepository repo) => this.userRepository = repo;

        // GET: api/User
        [HttpGet]
        [Authorize("User")]
        public User Get() => GetUser(HttpContext.User);

        // DELETE api/User
        [HttpDelete]
        [Authorize("User")]
        public IActionResult Delete()
        {
            var user = GetUser(HttpContext.User);
            userRepository.DeleteUser(user.Id);
            userRepository.Save();

            return Ok(new { ErrorMsg = "Usuario [" + user.Id + "] borrado" });
        }

        // PUT api/User
        [HttpPut]
        [Authorize("User")]
        public IActionResult Put([FromBody] User newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMsg = "No se ha proporcionado un usuario valido" });
            }

            var user = GetUser(HttpContext.User);
            newUser.Id = user.Id;
            var updatedUser = userRepository.UpdateUser(newUser);

            return Ok(updatedUser);
        }

        // GET api/User/cocteles
        [HttpGet]
        [Authorize("User")]
        [Route("cocteles")]
        public IEnumerable<int> GetCoctels() => GetUser(HttpContext.User).CoctelesFavList;

        // GET: api/User/ingredientes
        [HttpGet]
        [Authorize("User")]
        [Route("ingredientes")]
        public IEnumerable<string> GetIngredients() => GetUser(HttpContext.User).IngredientesFavList;










        // PUT api/User/cocteles
        [HttpPut]
        [Authorize("User")]
        [Route("cocteles")]
        public void AddCoctel([FromBody]int value)
        {
            var user = GetUser(HttpContext.User);
        }










        // DELETE api/Favorites/Coctel/2
        [HttpDelete("/Coctel/{id}")]
        public void DeleteCoctel(int id)
        {
            var user = GetUser(HttpContext.User);
        }

        // POST api/Favorites/Ingredient
        [HttpPost("/Ingredient")]
        public void AddIngredient([FromBody]int value)
        {
            var user = GetUser(HttpContext.User);
        }

        // DELETE api/Favorites/Ingredient/2
        [HttpDelete("/Ingredient/{id}")]
        public void DeleteIngredient(int id)
        {
            var user = GetUser(HttpContext.User);
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
