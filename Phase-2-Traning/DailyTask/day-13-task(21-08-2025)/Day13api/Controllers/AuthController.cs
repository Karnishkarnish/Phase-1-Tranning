using Microsoft.AspNetCore.Mvc;
using Day13api.Models;
using Day13api.Security;

namespace Day13api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(IConfiguration config)
        {
            _jwtService = new JwtService(config);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            if (login.Username == "Karnish" && login.Role == "Admin")
            {
                var token = _jwtService.GenerateToken(login.Username, "Admin");
                return Ok(new { token });
            }

            if (login.Username == "Officer1" && login.Role == "Officer")
            {
                var token = _jwtService.GenerateToken(login.Username, "Officer");
                return Ok(new { token });
            }

            return Unauthorized("Invalid username or role");
        }
    }
}
