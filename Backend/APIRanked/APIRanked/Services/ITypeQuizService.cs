using APIRanked.Models;

namespace APIRanked.Services
{
    public interface ITypeQuizService
    {
        Task<IEnumerable<TypeQuiz>> GetAllAsync();
        Task<TypeQuiz?> GetByIdAsync(int id);
        Task AddAsync(TypeQuiz typeQuiz);
        Task UpdateAsync(TypeQuiz typeQuiz);
        Task DeleteAsync(int id);
    }
}
