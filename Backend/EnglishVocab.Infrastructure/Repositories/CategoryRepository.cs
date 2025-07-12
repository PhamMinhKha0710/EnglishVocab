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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EnglishVocabDatabaseContext _context;

        public CategoryRepository(EnglishVocabDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Category> AddAsync(Category category)
        {
            category.DateCreated = DateTime.UtcNow;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<Dictionary<string, int>> GetCategoriesWithCountsAsync()
        {
            var categories = await _context.Categories
                .Select(c => new
                {
                    c.Name,
                    WordCount = c.Words.Count()
                })
                .ToDictionaryAsync(
                    c => c.Name,
                    c => c.WordCount
                );

            return categories;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(category.Id);
            if (existingCategory == null)
                return null;

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.DateModified = DateTime.UtcNow;

            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();
            return existingCategory;
        }
    }
} 