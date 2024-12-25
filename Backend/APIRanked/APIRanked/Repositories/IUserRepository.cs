﻿using APIRanked.Models;

namespace APIRanked.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        void Update(User user);
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}