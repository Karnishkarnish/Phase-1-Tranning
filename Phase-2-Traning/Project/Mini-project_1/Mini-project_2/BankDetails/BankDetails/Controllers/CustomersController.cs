using BankDetails.Data;
using BankDetails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankDetails.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CustomersController(AppDbContext context, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpGet("getCustomers")]
        public ActionResult<IEnumerable<object>> GetCustomers()
        {
            var customers = _context.Customers
                .Where(a => a.Age >= 30)
                .OrderByDescending(a => a.Age)
                .Select(c => new
                {
                    c.Name,
                    c.Age
                })
                .ToList();

            return Ok(customers);
        }

        [HttpPost("addCustomer")]
        public ActionResult<object> CreateCustomer(Customer customer)
        {
            if (customer.Age <= 18)
            {
                return BadRequest("Not Valid");
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            var result = new
            {
                customer.Name,
                customer.Age
            };

            return CreatedAtAction(nameof(GetCustomers), new { id = customer.CustomerId }, result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            string fastApiUrl = "http://127.0.0.1:8000/login";  // FastAPI URL

            var content = new StringContent(
                JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8,
                "application/json"
            );

            try
            {
                var response = await _httpClient.PostAsync(fastApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseBody);

                    var token = GenerateJwtToken( loginResponse);

                    return Ok(new
                    {
                        message = "Login successful",
                        token = token,
                        data = loginResponse
                    });
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    return Unauthorized(new { message = "Invalid credentials", error = errorResponse });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request", error = ex.Message });
            }
        }

        private string GenerateJwtToken(LoginResponse user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
            {
                throw new Exception("Secret key must be at least 32 characters long.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username), 
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
