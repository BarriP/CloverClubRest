using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CloverClubRest.Models;
using CloverClubRest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloverClubRest.Controllers
{
    //TODO SALT

    [Route("api/[controller]")]
    public class TokensController : Controller
    {
        private IUsersRepository usersRepository;
        private IConfiguration config;

        public TokensController(IUsersRepository repo, IConfiguration configuration)
        {
            usersRepository = repo;
            config = configuration;
        }

        // POST: api/Tokens/login
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMsg = "No se ha proporcionado un login valido" });
            }

            IActionResult response = Unauthorized();

            //Admin Login
            if (login.Password.Equals(config["Admin:Pass"]) && login.Email.Equals(config["Admin:Email"]))
            {
                return Ok(new {token = BuildAdminToken()});
            }

            //User Login
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        // POST: api/Tokens/register
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterModel register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMsg = "No se ha proporcionado un login valido" });
            }

            var userWithSameEmail = usersRepository.FindByEmail(register.Email);
            if (userWithSameEmail != null)
            {
                return BadRequest(new {ErrorMsg = "El email ya existe"});
            }

            var user = usersRepository.InsertUser(new User
            {
                Name = register.Name,
                Pass = register.Pass,
                Email = register.Email,
            });
            usersRepository.Save();

            return Ok(user);
        }

        private string BuildToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims,
                //4 horas de duracion, como en la Graph API de Microsoft
                expires: DateTime.Now.AddHours(4),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string BuildAdminToken()
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, "admin"),
                new Claim(JwtRegisteredClaimNames.Email, "Admin"),
                new Claim(JwtRegisteredClaimNames.GivenName, "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(LoginModel login)
        {
            var user = usersRepository.FindByEmail(login.Email);

            if (user == null) return null;

            return user.Pass.Equals(login.Password) ? user : null;
        }

        public class LoginModel
        {
            [Required]
            [MinLength(3)]
            public string Email { get; set; }
            [Required]
            [MinLength(3)]
            public string Password { get; set; }
        }

        public class RegisterModel
        {
            [Required]
            [MinLength(3)]
            public string Name { get; set; }
            [Required]
            [MinLength(3)]
            public string Email { get; set; }
            [Required]
            [MinLength(3)]
            public string Pass { get; set; }
        }
    }
}
