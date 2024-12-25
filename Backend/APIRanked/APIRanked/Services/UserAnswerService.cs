using APIRanked.Models;
using APIRanked.Repositories;

namespace APIRanked.Services
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly IUserAnswerRepository _userAnswerRepository;
        public UserAnswerService(IUserAnswerRepository userAnswerRepository)
        {
            _userAnswerRepository = userAnswerRepository;
        }

        public async Task AddAsync(UserAnswer userAnswer)
        {
            await _userAnswerRepository.AddAsync(userAnswer);
            await _userAnswerRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _userAnswerRepository.DeleteAsync(id);
            await _userAnswerRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserAnswer>> GetAllAsync()
        {
            return await _userAnswerRepository.GetAllAsync();
        }

        public async Task<UserAnswer?> GetByIdAsync(int id)
        {
            return await _userAnswerRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UserAnswer userAnswer)
        {
            _userAnswerRepository.Update(userAnswer);
            await _userAnswerRepository.SaveChangesAsync();
        }
    }
}
