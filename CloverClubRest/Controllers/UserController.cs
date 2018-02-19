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
        public IActionResult Get()
        {
            var user = GetUser(HttpContext.User);
            return Ok(user);
        }

        // DELETE api/User
        [HttpDelete]
        [Authorize("User")]
        public IActionResult Delete()
        {
            var user = GetUser(HttpContext.User);
            userRepository.DeleteUser(user.Id);
            userRepository.Save();

            return Ok(new {ErrorMsg = "Usuario [" + user.Id + "] borrado"});
        }

        // PUT api/User
        [HttpPut]
        [Authorize("User")]
        public IActionResult Put([FromBody] User newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(new {ErrorMsg = "No se ha proporcionado un usuario valido"});

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

        // POST api/User/cocteles
        [HttpPost]
        [Authorize("User")]
        [Route("cocteles")]
        public IActionResult AddCoctel([FromBody] int? coctelId)
        {
            if (coctelId == null)
                return BadRequest(new {ErrorMsg = "No se ha proporcionado un coctel valido"});

            var user = GetUser(HttpContext.User);

            if (!userRepository.AddCoctelFav(user, coctelId.Value))
                return BadRequest(new {ErrorMsg = $"Coctel [{coctelId}] ya existe"});

            userRepository.Save();
            return Ok(new {Msg = $"Coctel [{coctelId}] añadido"});

        }

        // POST api/User/ingredientes
        [HttpPost]
        [Authorize("User")]
        [Route("ingredientes")]
        public IActionResult AddIngredient([FromBody] string ingredienteId)
        {
            if (String.IsNullOrEmpty(ingredienteId))
                return BadRequest(new { ErrorMsg = "No se ha proporcionado un ingrediente valido" });

            var user = GetUser(HttpContext.User);

            if (!userRepository.AddIngredienteFav(user, ingredienteId))
                return BadRequest(new {ErrorMsg = $"Ingrediente [{ingredienteId}] ya existe"});

            userRepository.Save();
            return Ok(new { Msg = $"Ingrediente [{ingredienteId}] añadido" });

        }

        // DELETE api/User/cocteles/2
        [HttpDelete]
        [Authorize("User")]
        [Route("cocteles/{id}")]
        public IActionResult DeleteCoctel(int id)
        {
            var user = GetUser(HttpContext.User);

            if (!userRepository.RemoveCoctelFav(user, id))
                return NotFound(new { ErrorMsg = $"Coctel [{id}] no existe" });

            userRepository.Save();
            return Ok(new { Msg = $"Coctel [{id}] borrado" });
        }

        // DELETE api/User/ingredientes/2
        [HttpDelete]
        [Authorize("User")]
        [Route("ingredientes/{ingrediente}")]
        public IActionResult DeleteIngredient(string ingrediente)
        {
            var user = GetUser(HttpContext.User);

            if (!userRepository.RemoveIngredienteFav(user, ingrediente))
                return NotFound(new { ErrorMsg = $"Ingrediente [{ingrediente}] no existe" });

            userRepository.Save();
            return Ok(new { Msg = $"Ingrediente [{ingrediente}] borrado" });
        }

        private User GetUser(ClaimsPrincipal user)
        {
            string textId = user.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            return int.TryParse(textId, out int id) ? userRepository.GetUserById(id) : null;
        }
    }
}