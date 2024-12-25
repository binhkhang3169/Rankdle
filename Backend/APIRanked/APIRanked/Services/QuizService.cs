using APIRanked.Models;
using APIRanked.Repositories;

namespace APIRanked.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task AddAsync(Quiz quiz)
        {
            await _quizRepository.AddAsync(quiz);
            await _quizRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _quizRepository.DeleteAsync(id);
            await _quizRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return await _quizRepository.GetAllAsync();
        }

        public async Task<Quiz?> GetByIdAsync(int id)
        {
            return await _quizRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Quiz quiz)
        {
            _quizRepository.Update(quiz);
            await _quizRepository.SaveChangesAsync();
        }
    }
}
