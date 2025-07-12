using EnglishVocab.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IWordRepository
    {
        Task<Word> GetByIdAsync(int id);
        Task<IEnumerable<Word>> GetAllAsync();
        Task<IEnumerable<Word>> GetByIdsAsync(IEnumerable<int> ids);
        Task<IEnumerable<Word>> GetByCategoryAsync(string category);
        Task<IEnumerable<Word>> GetByDifficultyLevelAsync(string difficultyLevel);
        Task<IEnumerable<Word>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Word>> GetByCategoryIdAndDifficultyLevelAsync(int categoryId, string difficultyLevel);
        Task<IEnumerable<Word>> SearchAsync(string searchTerm);
        Task<IEnumerable<Word>> SearchAsync(string searchTerm, string category, string difficultyLevel, int pageNumber, int pageSize);
        Task<IEnumerable<Word>> GetPaginatedAsync(int pageNumber, int pageSize, string sortBy, bool ascending);
        Task<int> CountAsync();
        Task<Word> AddAsync(Word word);
        Task<Word> UpdateAsync(Word word);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<string>> GetAllCategoriesAsync();
        Task<Dictionary<string, int>> GetCategoriesWithCountsAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
} 