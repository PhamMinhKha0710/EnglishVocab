using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IWordService
    {
        Task<Word> GetWordByIdAsync(int id);
        Task<IEnumerable<Word>> GetAllWordsAsync();
        Task<IEnumerable<Word>> GetWordsByCategoryAsync(string category);
        Task<IEnumerable<Word>> GetWordsByDifficultyLevelAsync(DifficultyLevelType difficultyLevel);
        Task<IEnumerable<Word>> GetWordsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Word>> GetWordsByCategoryIdAndDifficultyLevelAsync(int categoryId, DifficultyLevelType difficultyLevel);
        Task<Word> CreateWordAsync(Word word);
        Task<Word> UpdateWordAsync(Word word);
        Task<bool> DeleteWordAsync(int id);
        Task<IEnumerable<Word>> SearchWordsAsync(string searchTerm);
    }
} 
 