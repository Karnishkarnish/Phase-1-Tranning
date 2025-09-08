using Microsoft.AspNetCore.Mvc;
using OrganicStore.Dtos;
using OrganicStore.Model;
using OrganicStore.Service;

namespace OrganicStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // ✅ Register with default "Customer" role if not provided
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var role = string.IsNullOrWhiteSpace(request.Role) ? "Customer" : request.Role;

            var response = await _authService.Register(
                new User
                {
                    Email = request.Email,
                    Name = request.Name,
                    Role = role
                },
                request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // ✅ Login returns JWT with role claim
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authService.Login(request.Email, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            // Ensure token is not empty
            if (string.IsNullOrWhiteSpace(response.Data))
            {
                return BadRequest(new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Failed to generate JWT"
                });
            }

            return Ok(response);
        }
    }
}
