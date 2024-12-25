using APIRanked.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRanked.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;
        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
        }

        public async Task DeleteAsync(int id)
        {
            var quiz = await GetByIdAsync(id);
            if (quiz != null)
            {
                _context.Quizzes.Remove(quiz);
            }
        }

        public async Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return await _context.Quizzes.ToListAsync();
        }

        public async Task<Quiz?> GetByIdAsync(int id)
        {
            return await _context.Quizzes.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Quiz quiz)
        {
            _context.Quizzes.Update(quiz);
        }
    }
}
