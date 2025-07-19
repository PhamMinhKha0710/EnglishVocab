using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnglishVocab.Domain.Entities;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IWordRepository
    {
        Task<List<Word>> GetAllAsync(
            string searchTerm = null, 
            int? categoryId = null,
            int? difficultyLevelId = null,
            CancellationToken cancellationToken = default);
            
        Task<List<Word>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
        Task<List<Word>> GetByDifficultyLevelAsync(int difficultyLevelId, CancellationToken cancellationToken = default);
        Task<List<Word>> GetByCategoryIdAndDifficultyLevelAsync(int categoryId, int difficultyLevelId, CancellationToken cancellationToken = default);
        Task<List<Word>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
        
        Task<Word> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<(Word Word, Category Category, DifficultyLevel DifficultyLevel)> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
        Task<List<(Word Word, Category Category, DifficultyLevel DifficultyLevel)>> GetAllWithDetailsAsync(
            string searchTerm = null, 
            int? categoryId = null,
            int? difficultyLevelId = null,
            CancellationToken cancellationToken = default);
        
        Task<Word> AddAsync(Word word, CancellationToken cancellationToken = default);
        Task<Word> UpdateAsync(Word word, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
} 