using APIRanked.Models;

namespace APIRanked.Repositories
{
    public interface IDailyRepository
    {
        Task<IEnumerable<Daily>> GetAllAsync();
        Task<Daily?> GetByIdAsync(int id);
        Task AddAsync(Daily daily);
        void Update(Daily daily);
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
