using APIRanked.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIRanked.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;

        public AuthService(IConfiguration configuration, IDistributedCache cache)
        {
            _configuration = configuration;
            _cache = cache;
        }

        // Tạo JWT Token
        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Lưu token vào cache với thời gian sống tương ứng
            var cacheExpiry = TimeSpan.FromMinutes(double.Parse(jwtSettings["ExpiryMinutes"]));
            await _cache.SetStringAsync(tokenString, user.UserId.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheExpiry
            });

            return tokenString;
        }

        // Xác thực Token
        public async Task<bool> ValidateTokenAsync(string token)
        {
            var userId = await _cache.GetStringAsync(token);
            return !string.IsNullOrEmpty(userId);
        }

        // Xóa Token (Logout)
        public async Task<bool> RevokeTokenAsync(string token)
        {
            var userId = await _cache.GetStringAsync(token);
            if (string.IsNullOrEmpty(userId))
                return false;

            await _cache.RemoveAsync(token);
            return true;
        }

        public async Task<string> GetUserByTokenAsync(string token)
        {
            var userId = await _cache.GetStringAsync(token);
            if (userId == null)
            {
                return "";
            }
            return userId;
        }
    }
}
