using APIRanked.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRanked.Repositories
{
    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly AppDbContext _context;
        public UserAnswerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserAnswer userAnswer)
        {
            await _context.UserAnswers.AddAsync(userAnswer);
        }

        public async Task DeleteAsync(int id)
        {
            var userAnswer = await GetByIdAsync(id);
            if (userAnswer != null)
            {
                _context.UserAnswers.Remove(userAnswer);
            }
        }

        public async Task<IEnumerable<UserAnswer>> GetAllAsync()
        {
            return await _context.UserAnswers.ToListAsync();
        }

        public async Task<UserAnswer?> GetByIdAsync(int id)
        {
            return await _context.UserAnswers.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(UserAnswer userAnswer)
        {
            _context.UserAnswers.Update(userAnswer);
        }
    }
}
