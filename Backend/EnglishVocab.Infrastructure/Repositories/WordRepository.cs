using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishVocab.Infrastructure.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public WordRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Word>> GetAllAsync(
            string searchTerm = null, 
            int? categoryId = null,
            int? difficultyLevelId = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Word> query = _dbContext.Words;

            // Apply filters
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(w => 
                    w.English.Contains(searchTerm) || 
                    w.Vietnamese.Contains(searchTerm) || 
                    (w.Example != null && w.Example.Contains(searchTerm))
                );
            }

            if (categoryId.HasValue)
            {
                query = query.Where(w => w.CategoryId == categoryId);
            }

            if (difficultyLevelId.HasValue)
            {
                query = query.Where(w => w.DifficultyLevelId == difficultyLevelId);
            }

            // Execute query and return results
            return await query.ToListAsync(cancellationToken);
        }
        
        public async Task<List<Word>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Words
                .Where(w => w.CategoryId == categoryId)
                .ToListAsync(cancellationToken);
        }
        
        public async Task<List<Word>> GetByDifficultyLevelAsync(int difficultyLevelId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Words
                .Where(w => w.DifficultyLevelId == difficultyLevelId)
                .ToListAsync(cancellationToken);
        }
        
        public async Task<List<Word>> GetByCategoryIdAndDifficultyLevelAsync(int categoryId, int difficultyLevelId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Words
                .Where(w => w.CategoryId == categoryId && w.DifficultyLevelId == difficultyLevelId)
                .ToListAsync(cancellationToken);
        }
        
        public async Task<List<Word>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Word>();
                
            return await _dbContext.Words
                .Where(w => 
                    w.English.Contains(searchTerm) || 
                    w.Vietnamese.Contains(searchTerm) || 
                    (w.Example != null && w.Example.Contains(searchTerm))
                )
                .ToListAsync(cancellationToken);
        }

        public async Task<Word> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Words
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
        }
        
        public async Task<(Word Word, Category Category, DifficultyLevel DifficultyLevel)> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
        {
            var word = await _dbContext.Words
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
                
            if (word == null)
                return (null, null, null);
                
            Category category = null;
            DifficultyLevel difficultyLevel = null;
            
            if (word.CategoryId.HasValue)
            {
                category = await _dbContext.Categories
                    .FirstOrDefaultAsync(c => c.Id == word.CategoryId.Value, cancellationToken);
            }
            
            if (word.DifficultyLevelId.HasValue)
            {
                difficultyLevel = await _dbContext.DifficultyLevels
                    .FirstOrDefaultAsync(d => d.Id == word.DifficultyLevelId.Value, cancellationToken);
            }
            
            return (word, category, difficultyLevel);
        }
        
        public async Task<List<(Word Word, Category Category, DifficultyLevel DifficultyLevel)>> GetAllWithDetailsAsync(
            string searchTerm = null, 
            int? categoryId = null,
            int? difficultyLevelId = null,
            CancellationToken cancellationToken = default)
        {
            // First get the words based on filters
            var words = await GetAllAsync(searchTerm, categoryId, difficultyLevelId, cancellationToken);
            
            // Then get the related data
            var result = new List<(Word Word, Category Category, DifficultyLevel DifficultyLevel)>();
            
            foreach (var word in words)
            {
                Category category = null;
                DifficultyLevel difficultyLevel = null;
                
                if (word.CategoryId.HasValue)
                {
                    category = await _dbContext.Categories
                        .FirstOrDefaultAsync(c => c.Id == word.CategoryId.Value, cancellationToken);
                }
                
                if (word.DifficultyLevelId.HasValue)
                {
                    difficultyLevel = await _dbContext.DifficultyLevels
                        .FirstOrDefaultAsync(d => d.Id == word.DifficultyLevelId.Value, cancellationToken);
                }
                
                result.Add((word, category, difficultyLevel));
            }
            
            return result;
        }

        public async Task<Word> AddAsync(Word word, CancellationToken cancellationToken = default)
        {
            await _dbContext.Words.AddAsync(word, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return word;
        }

        public async Task<Word> UpdateAsync(Word word, CancellationToken cancellationToken = default)
        {
            _dbContext.Words.Update(word);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return word;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var word = await _dbContext.Words.FindAsync(new object[] { id }, cancellationToken);
            if (word != null)
            {
                _dbContext.Words.Remove(word);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Words.AnyAsync(w => w.Id == id, cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Words.CountAsync(cancellationToken);
        }
    }
} 
 