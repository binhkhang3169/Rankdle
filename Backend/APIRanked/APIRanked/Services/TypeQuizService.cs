using APIRanked.Models;
using APIRanked.Repositories;

namespace APIRanked.Services
{
    public class TypeQuizService :ITypeQuizService
    {
        private readonly ITypeQuizRepository _typeQuizRepository;
        public TypeQuizService(ITypeQuizRepository typeQuizRepository)
        {
            _typeQuizRepository = typeQuizRepository;
        }

        public async Task AddAsync(TypeQuiz typeQuiz)
        {
            await _typeQuizRepository.AddAsync(typeQuiz);
            await _typeQuizRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _typeQuizRepository.DeleteAsync(id);
            await _typeQuizRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TypeQuiz>> GetAllAsync()
        {
            return await _typeQuizRepository.GetAllAsync();
        }

        public async Task<TypeQuiz?> GetByIdAsync(int id)
        {
            return await _typeQuizRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(TypeQuiz typeQuiz)
        {
            _typeQuizRepository.Update(typeQuiz);
            await _typeQuizRepository.SaveChangesAsync();
        }
    }
}
