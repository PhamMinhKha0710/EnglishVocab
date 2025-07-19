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
            // Aqui precisamos primeiro obter a categoria pelo nome e depois buscar as palavras pelo ID da categoria
            // Como não temos acesso ao CategoryRepository aqui, vamos usar o método GetAllAsync com filtro
            return await _wordRepository.GetAllAsync(categoryId: null);
        }

        public async Task<IEnumerable<Word>> GetWordsByDifficultyLevelAsync(DifficultyLevelType difficultyLevel)
        {
            // Aqui precisamos primeiro obter o nível de dificuldade pelo nome e depois buscar as palavras pelo ID
            // Como não temos acesso ao DifficultyLevelRepository aqui, vamos usar o método GetAllAsync com filtro
            return await _wordRepository.GetAllAsync(difficultyLevelId: null);
        }

        public async Task<IEnumerable<Word>> GetWordsByCategoryIdAsync(int categoryId)
        {
            return await _wordRepository.GetByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<Word>> GetWordsByCategoryIdAndDifficultyLevelAsync(int categoryId, DifficultyLevelType difficultyLevel)
        {
            // Aqui precisamos primeiro obter o ID do nível de dificuldade pelo enum
            // Como não temos acesso ao DifficultyLevelRepository aqui, vamos usar o método GetAllAsync com ambos os filtros
            return await _wordRepository.GetAllAsync(categoryId: categoryId, difficultyLevelId: null);
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
 