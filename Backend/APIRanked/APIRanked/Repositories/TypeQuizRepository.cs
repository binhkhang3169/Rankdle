using APIRanked.Models;
using Microsoft.EntityFrameworkCore;
namespace APIRanked.Repositories
{
    public class TypeQuizRepository : ITypeQuizRepository
    {
        private readonly AppDbContext _context;
        public TypeQuizRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TypeQuiz typeQuiz)
        {
            await _context.TypeQuizzes.AddAsync(typeQuiz);
        }

        public async Task DeleteAsync(int id)
        {
            var typeQuiz = await GetByIdAsync(id);
            if (typeQuiz != null)
            {
                _context.TypeQuizzes.Remove(typeQuiz);
            }
        }

        public async Task<IEnumerable<TypeQuiz>> GetAllAsync()
        {
            return await _context.TypeQuizzes.ToListAsync();
        }

        public async Task<TypeQuiz?> GetByIdAsync(int id)
        {
            return await _context.TypeQuizzes.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(TypeQuiz typeQuiz)
        {
            _context.TypeQuizzes.Update(typeQuiz);
        }
    }
}
