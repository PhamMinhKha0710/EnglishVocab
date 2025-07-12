using EnglishVocab.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IWordSetRepository
    {
        Task<WordSet> GetByIdAsync(int id);
        Task<IEnumerable<WordSet>> GetAllAsync();
        Task<IEnumerable<WordSet>> GetByUserIdAsync(string userId);
        Task<IEnumerable<WordSet>> GetDefaultSetsAsync();
        Task<IEnumerable<Word>> GetWordsBySetIdAsync(int wordSetId);
        Task<IEnumerable<WordSet>> GetPaginatedAsync(int pageIndex, int pageSize, string? sortBy = null, bool ascending = true);
        Task<WordSet> AddAsync(WordSet wordSet);
        Task<WordSet> UpdateAsync(WordSet wordSet);
        Task<bool> DeleteAsync(int id);
        Task<bool> AddWordToSetAsync(int wordSetId, int wordId);
        Task<bool> RemoveWordFromSetAsync(int wordSetId, int wordId);
        Task<int> CountAsync();
        Task<IEnumerable<int>> GetWordIdsBySetIdAsync(int wordSetId);
    }
} 