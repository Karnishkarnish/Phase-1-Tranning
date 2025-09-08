using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrganicStore.Dtos;
using OrganicStore.Model;
using OrganicStoreApplication.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrganicStore.Service
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return new ServiceResponse<int> { Success = false, Message = "User already exists" };
            }

            CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            if (string.IsNullOrWhiteSpace(user.Role))
                user.Role = "Customer"; // ✅ default role

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = user.Id, Success = true, Message = "User registered" };
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return new ServiceResponse<string> { Success = false, Message = "Invalid credentials" };
            }

            if (string.IsNullOrWhiteSpace(user.Role))
            {
                user.Role = "Customer"; // ✅ enforce default
                await _context.SaveChangesAsync();
            }

            var token = GenerateJwtToken(user);

            return new ServiceResponse<string> { Data = token, Success = true, Message = "Login successful" };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role) // ✅ role claim
            };

            // ✅ Add custom claims for storeId and address
            if (user.StoreId.HasValue)
                claims.Add(new Claim("storeId", user.StoreId.Value.ToString()));
            if (user.StoreId != null)
            {
                 claims.Add(new Claim("storeId", user.StoreId.ToString()));
            }


            if (!string.IsNullOrEmpty(user.Address))
                claims.Add(new Claim("address", user.Address));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }
}
