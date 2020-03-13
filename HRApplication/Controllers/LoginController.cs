using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HRApplication.Data;
using HRApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HRApplication.Controllers
{
    public class LoginController : Controller
    {
        public IConfiguration Configuration;
        public AppDbContext AppDbContext { get; set; }

        public LoginController(IConfiguration configuration, AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            Configuration = configuration;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(string Username,string Password,int Role)
        {
            try
            {
                string mysalt = BCrypt.Net.BCrypt.GenerateSalt();
                var BPassword = BCrypt.Net.BCrypt.HashPassword(Password, mysalt);
                User users = new User()
                {
                    Username = Username,
                    Password = BPassword,
                    Role = Role,
                    Created_at = DateTime.Now
                };
                AppDbContext.User.Add(users);
                AppDbContext.SaveChanges();
                return RedirectToAction("Index","Login");
            }
            catch
            {
                return View();
            }
        }
        // POST: Login/Login
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            try
            {
                IActionResult response = Unauthorized();

                var user = AuthenticatedUser(Username, Password);
                if (user != null)
                {
                    if (user.Role == 1)
                    {
                        var token = GenerateJwtToken(user);
                        HttpContext.Session.SetString("JWTToken", token);
                        HttpContext.Session.SetString("Id", user.Id.ToString());
                        HttpContext.Session.SetString("Name", user.Username.ToString());
                        var get = HttpContext.Session.GetString("JWTToken");
                        return RedirectToAction("Index","Home");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                // new Claim(JwtRegisteredClaimNames.Sub,user.username),
                new Claim(JwtRegisteredClaimNames.Sub,Convert.ToString(user.Username)),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedToken;
        }

        private User AuthenticatedUser(string Username, string Password)
        {
            User user = null;
            var x = (from data in AppDbContext.User where data.Username == Username orderby data.Id select new { data.Username, data.Password, data.Id, data.Role }).LastOrDefault();
            var verify = BCrypt.Net.BCrypt.Verify(Password, x.Password);
            if (x.Username == Username && (verify == true))
            {
                user = new User
                {
                    Id = x.Id,
                    Username = Username,
                    Role = x.Role,
                    Password = x.Password,
                };
            }
            return user;
        }
    }
}