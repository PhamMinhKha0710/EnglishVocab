using EnglishVocab.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IWordSetWordRepository
    {
        Task<WordSetWord> GetByIdAsync(int id);
        Task<IEnumerable<WordSetWord>> GetAllAsync();
        Task<IEnumerable<WordSetWord>> GetByWordSetIdAsync(int wordSetId);
        Task<IEnumerable<WordSetWord>> GetByWordIdAsync(int wordId);
        Task<WordSetWord> GetByWordIdAndWordSetIdAsync(int wordId, int wordSetId);
        Task<WordSetWord> AddAsync(WordSetWord wordSetWord);
        Task<WordSetWord> UpdateAsync(WordSetWord wordSetWord);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByWordIdAndWordSetIdAsync(int wordId, int wordSetId);
        Task<int> CountByWordSetIdAsync(int wordSetId);
    }
} 