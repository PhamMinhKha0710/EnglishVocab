using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Domain.Entities;
using EnglishVocab.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishVocab.Infrastructure.Repositories
{
    public class WordSetRepository : IWordSetRepository
    {
        private readonly EnglishVocabDatabaseContext _context;

        public WordSetRepository(EnglishVocabDatabaseContext context)
        {
            _context = context;
        }

        public async Task<WordSet> AddAsync(WordSet wordSet)
        {
            wordSet.DateCreated = DateTime.UtcNow;
            wordSet.DateModified = DateTime.UtcNow;
            await _context.WordSets.AddAsync(wordSet);
            await _context.SaveChangesAsync();
            return wordSet;
        }

        public async Task<bool> AddWordToSetAsync(int wordSetId, int wordId)
        {
            var wordSet = await _context.WordSets
                .FindAsync(wordSetId);

            if (wordSet == null)
                return false;

            var word = await _context.Words.FindAsync(wordId);
            if (word == null)
                return false;

            var wordSetWord = new WordSetWord
            {
                WordSetId = wordSetId,
                WordId = wordId
            };

            await _context.WordSetWords.AddAsync(wordSetWord);
            wordSet.DateModified = DateTime.UtcNow;
            wordSet.WordCount++;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var wordSet = await _context.WordSets.FindAsync(id);
            if (wordSet == null)
                return false;

            _context.WordSets.Remove(wordSet);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<WordSet>> GetAllAsync()
        {
            return await _context.WordSets.ToListAsync();
        }

        public async Task<WordSet> GetByIdAsync(int id)
        {
            return await _context.WordSets
                .FirstOrDefaultAsync(ws => ws.Id == id);
        }

        public async Task<IEnumerable<WordSet>> GetByUserIdAsync(string userId)
        {
            return await _context.WordSets
                .Where(ws => ws.CreatedByUserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<WordSet>> GetDefaultSetsAsync()
        {
            return await _context.WordSets
                .Where(ws => ws.IsDefault)
                .ToListAsync();
        }

        public async Task<IEnumerable<Word>> GetWordsBySetIdAsync(int wordSetId)
        {
            var wordIds = await _context.WordSetWords
                .Where(wsw => wsw.WordSetId == wordSetId)
                .Select(wsw => wsw.WordId)
                .ToListAsync();

            return await _context.Words
                .Where(w => wordIds.Contains(w.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetWordIdsBySetIdAsync(int wordSetId)
        {
            return await _context.WordSetWords
                .Where(wsw => wsw.WordSetId == wordSetId)
                .Select(wsw => wsw.WordId)
                .ToListAsync();
        }

        public async Task<bool> RemoveWordFromSetAsync(int wordSetId, int wordId)
        {
            var wordSetWord = await _context.WordSetWords
                .FirstOrDefaultAsync(wsw => wsw.WordSetId == wordSetId && wsw.WordId == wordId);

            if (wordSetWord == null)
                return false;

            _context.WordSetWords.Remove(wordSetWord);
            
            var wordSet = await _context.WordSets.FindAsync(wordSetId);
            if (wordSet != null)
            {
                wordSet.DateModified = DateTime.UtcNow;
                wordSet.WordCount = Math.Max(0, wordSet.WordCount - 1);
            }
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<WordSet> UpdateAsync(WordSet wordSet)
        {
            var existingWordSet = await _context.WordSets.FindAsync(wordSet.Id);
            if (existingWordSet == null)
                return null;

            existingWordSet.Name = wordSet.Name;
            existingWordSet.Description = wordSet.Description;
            existingWordSet.IsPublic = wordSet.IsPublic;
            existingWordSet.DateModified = DateTime.UtcNow;

            _context.WordSets.Update(existingWordSet);
            await _context.SaveChangesAsync();
            return existingWordSet;
        }

        public async Task<IEnumerable<WordSet>> GetPaginatedAsync(int pageIndex, int pageSize, string? sortBy = null, bool ascending = true)
        {
            IQueryable<WordSet> query = _context.WordSets;

            // Sắp xếp
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        query = ascending ? query.OrderBy(ws => ws.Name) : query.OrderByDescending(ws => ws.Name);
                        break;
                    case "wordcount":
                        query = ascending ? query.OrderBy(ws => ws.WordCount) : query.OrderByDescending(ws => ws.WordCount);
                        break;
                    case "createdat":
                    case "datecreated":
                        query = ascending ? query.OrderBy(ws => ws.DateCreated) : query.OrderByDescending(ws => ws.DateCreated);
                        break;
                    default:
                        query = ascending ? query.OrderBy(ws => ws.Id) : query.OrderByDescending(ws => ws.Id);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(ws => ws.DateCreated);
            }

            return await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.WordSets.CountAsync();
        }
    }
} 
 