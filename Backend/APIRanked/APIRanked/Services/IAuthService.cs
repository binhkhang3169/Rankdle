using APIRanked.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIRanked.Services
{
    public interface IAuthService
    {

        // Tạo JWT Token
        Task<string> GenerateJwtTokenAsync(User user);

        // Xác thực Token
        Task<bool> ValidateTokenAsync(string token);
        Task<string> GetUserByTokenAsync(string token);

        // Xóa Token (Logout)
        Task<bool> RevokeTokenAsync(string token);
    }
}
