using APIRanked.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRanked.Repositories
{
    public class DailyRepository : IDailyRepository
    {
        private readonly AppDbContext _context;

        public DailyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Daily>> GetAllAsync()
        {
            return await _context.Dailies.ToListAsync();
        }

        public async Task<Daily?> GetByIdAsync(int id)
        {
            return await _context.Dailies.FindAsync(id);
        }

        public async Task AddAsync(Daily daily)
        {
            await _context.Dailies.AddAsync(daily);
        }

        public void Update(Daily daily)
        {
            _context.Dailies.Update(daily);
        }

        public async Task DeleteAsync(int id)
        {
            var daily = await GetByIdAsync(id);
            if (daily != null)
            {
                _context.Dailies.Remove(daily);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
