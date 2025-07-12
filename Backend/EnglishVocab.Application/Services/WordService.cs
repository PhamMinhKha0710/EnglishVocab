using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishVocab.Constants.Constant;

namespace EnglishVocab.Application.Services
{
    public class WordService : IWordService
    {
        private readonly IWordRepository _wordRepository;

        public WordService(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public async Task<Word> GetWordByIdAsync(int id)
        {
            return await _wordRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Word>> GetAllWordsAsync()
        {
            return await _wordRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Word>> GetWordsByCategoryAsync(string category)
        {
            return await _wordRepository.GetByCategoryAsync(category);
        }

        public async Task<IEnumerable<Word>> GetWordsByDifficultyLevelAsync(DifficultyLevelType difficultyLevel)
        {
            return await _wordRepository.GetByDifficultyLevelAsync(difficultyLevel.ToString());
        }

        public async Task<IEnumerable<Word>> GetWordsByCategoryIdAsync(int categoryId)
        {
            return await _wordRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Word>> GetWordsByCategoryIdAndDifficultyLevelAsync(int categoryId, DifficultyLevelType difficultyLevel)
        {
            return await _wordRepository.GetByCategoryIdAndDifficultyLevelAsync(categoryId, difficultyLevel.ToString());
        }

        public async Task<Word> CreateWordAsync(Word word)
        {
            return await _wordRepository.AddAsync(word);
        }

        public async Task<Word> UpdateWordAsync(Word word)
        {
            return await _wordRepository.UpdateAsync(word);
        }

        public async Task<bool> DeleteWordAsync(int id)
        {
            return await _wordRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Word>> SearchWordsAsync(string searchTerm)
        {
            return await _wordRepository.SearchAsync(searchTerm);
        }
    }
} 
 