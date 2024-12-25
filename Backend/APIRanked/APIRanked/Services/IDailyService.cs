﻿using APIRanked.Models;

namespace APIRanked.Services
{
    public interface IDailyService
    {
        Task<IEnumerable<Daily>> GetAllAsync();
        Task<Daily?> GetByIdAsync(int id);
        Task AddAsync(Daily daily);
        Task UpdateAsync(Daily daily);
        Task DeleteAsync(int id);
    }
}