using APIRanked.Models;

namespace APIRanked.Repositories
{
    public interface IUserAnswerRepository
    {
        Task<IEnumerable<UserAnswer>> GetAllAsync();
        Task<UserAnswer?> GetByIdAsync(int id);
        Task AddAsync(UserAnswer userAnswer);
        void Update(UserAnswer userAnswer);
        Task DeleteAsync(int id); 
        Task<bool> SaveChangesAsync();
    }
}
