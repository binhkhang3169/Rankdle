using APIRanked.Models;

namespace APIRanked.Repositories
{
    public interface IDailyRepository
    {
        Task<IEnumerable<Daily>> GetAllAsync();
        Task<Daily?> GetByIdAsync(int typeId, DateOnly date);
        Task AddAsync(Daily daily);
        void Update(Daily daily);
        Task DeleteAsync(int typeId, DateOnly date);
        Task<bool> SaveChangesAsync();
    }
}
