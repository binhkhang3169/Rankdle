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

        public async Task<Daily?> GetByIdAsync(int typeId, DateOnly date)
        {
            return await _context.Dailies
                .FirstOrDefaultAsync(d => d.TypeId == typeId && d.Date == date);
        }

        public async Task AddAsync(Daily daily)
        {
            await _context.Dailies.AddAsync(daily);
        }

        public void Update(Daily daily)
        {
            _context.Dailies.Update(daily);
        }

        public async Task DeleteAsync(int typeId, DateOnly date)
        {
            var daily = await GetByIdAsync(typeId, date);
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
