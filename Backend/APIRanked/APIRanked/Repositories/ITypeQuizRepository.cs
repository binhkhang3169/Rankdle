using APIRanked.Models;

namespace APIRanked.Repositories
{
    public interface ITypeQuizRepository
    {
        Task<IEnumerable<TypeQuiz>> GetAllAsync();
        Task<TypeQuiz?> GetByIdAsync(int id);
        Task AddAsync(TypeQuiz typeQuiz);
        void Update(TypeQuiz typeQuiz);
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
