using EnglishVocab.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(int id);
        Task<Category> GetByNameAsync(string name);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(string name);
        Task<Dictionary<string, int>> GetCategoriesWithCountsAsync();
    }
} 