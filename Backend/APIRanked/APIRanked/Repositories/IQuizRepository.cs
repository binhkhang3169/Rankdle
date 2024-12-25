using APIRanked.Models;

namespace APIRanked.Repositories
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetAllAsync();
        Task<Quiz?> GetByIdAsync(int id);
        Task AddAsync(Quiz quiz);
        void Update(Quiz quiz);
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
