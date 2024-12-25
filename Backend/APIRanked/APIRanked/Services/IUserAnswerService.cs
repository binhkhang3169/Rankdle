using APIRanked.Models;

namespace APIRanked.Services
{
    public interface IUserAnswerService
    {
        Task<IEnumerable<UserAnswer>> GetAllAsync();
        Task<UserAnswer?> GetByIdAsync(int id);
        Task AddAsync(UserAnswer userAnswer);
        Task UpdateAsync(UserAnswer userAnswer);
        Task DeleteAsync(int id);
    }
}
