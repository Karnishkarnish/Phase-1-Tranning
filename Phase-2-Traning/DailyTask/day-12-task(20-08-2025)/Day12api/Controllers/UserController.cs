using Day12api.Context;
using Day12api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Day12api.Controllers
{
    public class UserController : Controller
    {
        MyAppDbContext appDbContext;
        public UserController(MyAppDbContext ctxt)
        {
             appDbContext = ctxt;
        }
        [HttpPost("AddUser")]   
        public IActionResult AddUser(UserDTO user)
        {
            user.Password = GetHashPassword(user.Password);
            appDbContext.Users.Add(user);
              appDbContext.SaveChanges();
              return Ok("User added successfully");
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = appDbContext.Users.ToList();
                     return Ok(users);
        }
        [HttpGet("GetAllUsers_v2")]
        public IActionResult GetAllUsers_v2()
        {
            var users = appDbContext.Users
             .Select(x => new { x.Username, x.Email })
             .ToList();

            return Ok(users);
        }
        private string GetHashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] passBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha.ComputeHash(passBytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
