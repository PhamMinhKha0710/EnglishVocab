using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EnglishVocab.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public CategoryRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Categories.ToListAsync(cancellationToken);
        }

        public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Categories.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower(), cancellationToken);
        }

        public async Task<Category> AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _dbContext.Categories.AddAsync(category, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return category;
        }

        public async Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return category;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var category = await _dbContext.Categories.FindAsync(new object[] { id }, cancellationToken);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }
        
        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Categories.CountAsync(cancellationToken);
        }
    }
} 