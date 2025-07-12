using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Entities;
using EnglishVocab.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Infrastructure.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly EnglishVocabDatabaseContext _context;

        public WordRepository(EnglishVocabDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Word> AddAsync(Word word)
        {
            word.DateCreated = DateTime.UtcNow;
            word.DateModified = DateTime.UtcNow;
            await _context.Words.AddAsync(word);
            await _context.SaveChangesAsync();
            return word;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Words.CountAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var word = await _context.Words.FindAsync(id);
            if (word == null)
                return false;

            _context.Words.Remove(word);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Word>> GetAllAsync()
        {
            return await _context.Words.ToListAsync();
        }

        public async Task<IEnumerable<Word>> GetByCategoryAsync(string category)
        {
            return await _context.Words
                .Where(w => w.Category == category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Word>> GetByDifficultyLevelAsync(string difficultyLevel)
        {
            if (Enum.TryParse<Constants.Constant.DifficultyLevelType>(difficultyLevel, out var level))
            {
                return await _context.Words
                    .Where(w => w.DifficultyLevel == level)
                    .ToListAsync();
            }
            return new List<Word>();
        }

        public async Task<Word> GetByIdAsync(int id)
        {
            return await _context.Words.FindAsync(id);
        }
        
        public async Task<IEnumerable<Word>> GetByIdsAsync(IEnumerable<int> wordIds)
        {
            return await _context.Words
                .Where(w => wordIds.Contains(w.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Word>> GetByWordSetIdAsync(int wordSetId)
        {
            // Get wordIds from the join table
            var wordIds = await _context.WordSetWords
                .Where(wsw => wsw.WordSetId == wordSetId)
                .Select(wsw => wsw.WordId)
                .ToListAsync();
            
            // Get words by IDs
            return await _context.Words
                .Where(w => wordIds.Contains(w.Id))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<string>> GetAllCategoriesAsync()
        {
            return await _context.Words
                .Select(w => w.Category)
                .Distinct()
                .Where(c => !string.IsNullOrEmpty(c))
                .OrderBy(c => c)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetCategoriesWithCountsAsync()
        {
            // Query to get categories with their word counts
            var categoryCounts = await _context.Words
                .GroupBy(w => w.Category)
                .Select(g => new 
                { 
                    Category = g.Key, 
                    Count = g.Count() 
                })
                .Where(c => !string.IsNullOrEmpty(c.Category))
                .OrderBy(c => c.Category)
                .ToDictionaryAsync(
                    c => c.Category,
                    c => c.Count
                );
            
            return categoryCounts;
        }

        public async Task<IEnumerable<Word>> GetPaginatedAsync(int pageIndex, int pageSize, string? sortBy = null, bool ascending = true)
        {
            IQueryable<Word> query = _context.Words;

            // Apply sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "english":
                        query = ascending ? query.OrderBy(w => w.English) : query.OrderByDescending(w => w.English);
                        break;
                    case "vietnamese":
                        query = ascending ? query.OrderBy(w => w.Vietnamese) : query.OrderByDescending(w => w.Vietnamese);
                        break;
                    case "difficulty":
                        query = ascending ? query.OrderBy(w => w.DifficultyLevel) : query.OrderByDescending(w => w.DifficultyLevel);
                        break;
                    case "category":
                        query = ascending ? query.OrderBy(w => w.Category) : query.OrderByDescending(w => w.Category);
                        break;
                    case "frequency":
                        query = ascending ? query.OrderBy(w => w.Frequency) : query.OrderByDescending(w => w.Frequency);
                        break;
                    case "createdat":
                    case "datecreated":
                        query = ascending ? query.OrderBy(w => w.DateCreated) : query.OrderByDescending(w => w.DateCreated);
                        break;
                    default:
                        query = ascending ? query.OrderBy(w => w.Id) : query.OrderByDescending(w => w.Id);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(w => w.Id);
            }

            // Apply pagination
            return await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Word>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return new List<Word>();

            return await _context.Words
                .Where(w => w.English.Contains(searchTerm) || 
                           w.Vietnamese.Contains(searchTerm) ||
                           (w.Example != null && w.Example.Contains(searchTerm)))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Word>> SearchAsync(string searchTerm, string category, string difficultyLevel, int pageIndex, int pageSize)
        {
            IQueryable<Word> query = _context.Words;
            
            // Apply search term filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(w => w.English.Contains(searchTerm) || 
                                        w.Vietnamese.Contains(searchTerm) || 
                                        (w.Example != null && w.Example.Contains(searchTerm)));
            }
            
            // Apply category filter
            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                query = query.Where(w => w.Category == category);
            }
            
            // Apply difficulty level filter
            if (!string.IsNullOrEmpty(difficultyLevel) && difficultyLevel != "All" && 
                Enum.TryParse<Constants.Constant.DifficultyLevelType>(difficultyLevel, out var level))
            {
                query = query.Where(w => w.DifficultyLevel == level);
            }
            
            // Apply pagination
            return await query
                .OrderBy(w => w.English)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Word> UpdateAsync(Word word)
        {
            var existingWord = await _context.Words.FindAsync(word.Id);
            if (existingWord == null)
                return null;

            existingWord.English = word.English;
            existingWord.Vietnamese = word.Vietnamese;
            existingWord.Pronunciation = word.Pronunciation;
            existingWord.Example = word.Example;
            existingWord.ExampleTranslation = word.ExampleTranslation;
            existingWord.AudioUrl = word.AudioUrl;
            existingWord.ImageUrl = word.ImageUrl;
            existingWord.DifficultyLevel = word.DifficultyLevel;
            existingWord.Category = word.Category;
            existingWord.Frequency = word.Frequency;
            existingWord.Notes = word.Notes;
            existingWord.DateModified = DateTime.UtcNow;

            _context.Words.Update(existingWord);
            await _context.SaveChangesAsync();
            return existingWord;
        }

        public async Task<IEnumerable<Word>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Words
                .Where(w => w.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Word>> GetByCategoryIdAndDifficultyLevelAsync(int categoryId, string difficultyLevel)
        {
            if (Enum.TryParse<Constants.Constant.DifficultyLevelType>(difficultyLevel, out var level))
            {
                return await _context.Words
                    .Where(w => w.CategoryId == categoryId && w.DifficultyLevel == level)
                    .ToListAsync();
            }
            return new List<Word>();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
} 
 