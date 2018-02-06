using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloverClubRest.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Llamada a todos los usuarios";
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "Usario con id [" + id + "]";
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]string value)
        {
            return "Añadido usuario [" + value + "]";
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody]string value)
        {
            return "Añadido usuario [" + value + "]" + " con id [" + id + "]";
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return "Borrado usuario con id [" + id + "]";
        }
    }
}
