using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using CloverClubRest.Models;
using CloverClubRest.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloverClubRest.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private IUsersRepository usersRepository;

        public const int PAGE_SIZE = 50;

        public AdminController(IUsersRepository usersRepository) => this.usersRepository= usersRepository;

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get(int? page)
        {
            int pagenum = page ?? 0;
            if (pagenum < 0) pagenum = 0;

            var users = usersRepository.GetUsers().Skip(pagenum * PAGE_SIZE).Take(PAGE_SIZE);

            return users;
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = usersRepository.GetUserById(id);
            if (user == null)
                return NotFound(new {ErrorMsg = "User with ID [" + id + "] not found"});
            else
                return Ok(user);
        }

        // POST api/Users
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new {ErrorMsg = "No se ha proporcionado un usuario valido"});
            }

            var newUser = usersRepository.InsertUser(user);
            usersRepository.Save();

            return Ok(newUser);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]User value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMsg = "No se ha proporcionado un usuario valido" });
            }

            value.Id = id;

            var newUser = usersRepository.UpdateUser(value);

            if(newUser == null)
                return NotFound(new { ErrorMsg = "No existe usuario con id [" + id + "]" });

            usersRepository.Save();

            return Ok(newUser);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = usersRepository.DeleteUser(id);

            if (deleted)
            {
                usersRepository.Save();
                return Ok(new {ErrorMsg = "Usuario [" + id + "] borrado"});
            }
            else
                return NotFound(new {ErrorMsg = "No existe usuario con id [" + id + "]"});
        }

        protected override void Dispose(bool disposing)
        {
            usersRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
