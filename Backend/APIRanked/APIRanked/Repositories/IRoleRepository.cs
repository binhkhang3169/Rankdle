using APIRanked.Models;

namespace APIRanked.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(int id);
        Task AddAsync(Role role);
        void Update(Role role);
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
