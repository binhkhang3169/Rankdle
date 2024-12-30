using APIRanked.Models;
using APIRanked.Repositories;

namespace APIRanked.Services
{
    public class DailyService : IDailyService
    {
        private readonly IDailyRepository _dailyRepository;

        public DailyService(IDailyRepository dailyRepository)
        {
            _dailyRepository = dailyRepository;
        }

        public async Task AddAsync(Daily daily)
        {
            await _dailyRepository.AddAsync(daily);
            await _dailyRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int typeId, DateOnly date)
        {
            await _dailyRepository.DeleteAsync(typeId, date);
            await _dailyRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Daily>> GetAllAsync()
        {
            return await _dailyRepository.GetAllAsync();
        }

        public async Task<Daily?> GetByIdAsync(int typeId, DateOnly date)
        {
            return await _dailyRepository.GetByIdAsync(typeId, date);
        }

        public async Task UpdateAsync(Daily daily)
        {
            _dailyRepository.Update(daily);
            await _dailyRepository.SaveChangesAsync();
        }
    }
}
