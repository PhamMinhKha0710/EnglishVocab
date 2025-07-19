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
    public class DifficultyLevelRepository : IDifficultyLevelRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public DifficultyLevelRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<DifficultyLevel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.DifficultyLevels.ToListAsync(cancellationToken);
        }

        public async Task<IQueryable<DifficultyLevel>> GetAllDifficultyLevelsAsync(string searchTerm = null)
        {
            IQueryable<DifficultyLevel> query = _dbContext.DifficultyLevels;
            
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(d => 
                    d.Name.Contains(searchTerm) || 
                    (d.Description != null && d.Description.Contains(searchTerm))
                );
            }
            
            return query;
        }

        public async Task<DifficultyLevel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.DifficultyLevels.FindAsync(new object[] { id }, cancellationToken);
        }
        
        public async Task<DifficultyLevel> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.DifficultyLevels
                .FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower(), cancellationToken);
        }

        public async Task<DifficultyLevel> AddAsync(DifficultyLevel difficultyLevel, CancellationToken cancellationToken = default)
        {
            await _dbContext.DifficultyLevels.AddAsync(difficultyLevel, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return difficultyLevel;
        }

        public async Task<DifficultyLevel> UpdateAsync(DifficultyLevel difficultyLevel, CancellationToken cancellationToken = default)
        {
            _dbContext.DifficultyLevels.Update(difficultyLevel);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return difficultyLevel;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var difficultyLevel = await _dbContext.DifficultyLevels.FindAsync(new object[] { id }, cancellationToken);
            if (difficultyLevel != null)
            {
                _dbContext.DifficultyLevels.Remove(difficultyLevel);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
                return false;
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.DifficultyLevels.CountAsync(cancellationToken);
        }
    }
} 