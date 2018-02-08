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
    public class UsersController : Controller
    {
        private IUsersRepository usersRepository;

        public const int PAGE_SIZE = 25;

        public UsersController(IUsersRepository usersRepository) => this.usersRepository= usersRepository;

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

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]string value)
        {
            return "test";
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody]string value)
        {
            return "test";
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return "test";
        }

        protected override void Dispose(bool disposing)
        {
            usersRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
