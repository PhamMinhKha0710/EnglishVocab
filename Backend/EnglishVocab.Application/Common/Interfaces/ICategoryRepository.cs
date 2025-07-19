using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnglishVocab.Domain.Entities;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Category> AddAsync(Category category, CancellationToken cancellationToken = default);
        Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
} 