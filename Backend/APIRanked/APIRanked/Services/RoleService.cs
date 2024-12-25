using APIRanked.Models;
using APIRanked.Repositories;

namespace APIRanked.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task AddAsync(Role role)
        {
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _roleRepository.DeleteAsync(id);
            await _roleRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Role role)
        {
            _roleRepository.Update(role);
            await _roleRepository.SaveChangesAsync();
        }
    }
}
