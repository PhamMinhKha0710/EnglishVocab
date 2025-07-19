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
    public class WordSetWordRepository : IWordSetWordRepository
    {
        private readonly EnglishVocabDatabaseContext _context;

        public WordSetWordRepository(EnglishVocabDatabaseContext context)
        {
            _context = context;
        }

        public async Task<WordSetWord> GetByIdAsync(int id)
        {
            return await _context.WordSetWords.FindAsync(id);
        }

        public async Task<IEnumerable<WordSetWord>> GetAllAsync()
        {
            return await _context.WordSetWords.ToListAsync();
        }

        public async Task<IEnumerable<WordSetWord>> GetByWordSetIdAsync(int wordSetId)
        {
            return await _context.WordSetWords
                .Where(wsw => wsw.WordSetId == wordSetId)
                .ToListAsync();
        }

        public async Task<IEnumerable<WordSetWord>> GetByWordIdAsync(int wordId)
        {
            return await _context.WordSetWords
                .Where(wsw => wsw.WordId == wordId)
                .ToListAsync();
        }

        public async Task<WordSetWord> GetByWordIdAndWordSetIdAsync(int wordId, int wordSetId)
        {
            return await _context.WordSetWords
                .FirstOrDefaultAsync(wsw => wsw.WordId == wordId && wsw.WordSetId == wordSetId);
        }

        public async Task<WordSetWord> AddAsync(WordSetWord wordSetWord)
        {
            wordSetWord.AddedDate = DateTime.UtcNow;
            await _context.WordSetWords.AddAsync(wordSetWord);
            await _context.SaveChangesAsync();
            return wordSetWord;
        }

        public async Task<WordSetWord> UpdateAsync(WordSetWord wordSetWord)
        {
            _context.WordSetWords.Update(wordSetWord);
            await _context.SaveChangesAsync();
            return wordSetWord;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var wordSetWord = await _context.WordSetWords.FindAsync(id);
            if (wordSetWord == null)
                return false;

            _context.WordSetWords.Remove(wordSetWord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByWordIdAndWordSetIdAsync(int wordId, int wordSetId)
        {
            var wordSetWord = await _context.WordSetWords
                .FirstOrDefaultAsync(wsw => wsw.WordId == wordId && wsw.WordSetId == wordSetId);
                
            if (wordSetWord == null)
                return false;

            _context.WordSetWords.Remove(wordSetWord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> CountByWordSetIdAsync(int wordSetId)
        {
            return await _context.WordSetWords
                .CountAsync(wsw => wsw.WordSetId == wordSetId);
        }
    }
} 