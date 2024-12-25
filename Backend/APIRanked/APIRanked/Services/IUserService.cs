using APIRanked.Models;

namespace APIRanked.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<bool> IsUsernameTaken(string username);
        Task<bool> IsEmailTaken(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByResetTokenAsync(string resetToken);
        Task SendPasswordResetEmail(string email, string resetToken);
        string GenerateJwtToken(User user);
    }
}
