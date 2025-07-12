using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
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
    public class DifficultyLevelRepository : IDifficultyLevelRepository
    {
        private readonly EnglishVocabDatabaseContext _context;

        public DifficultyLevelRepository(EnglishVocabDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DifficultyLevelDto>> GetAllDifficultyLevelsAsync(CancellationToken cancellationToken = default)
        {
            var difficultyLevels = await _context.DifficultyLevels
                .Select(d => new DifficultyLevelDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Value = d.Value
                })
                .ToListAsync(cancellationToken);

            return difficultyLevels;
        }

        public async Task<DataTableResponse<DifficultyLevelDto>> GetPaginatedDifficultyLevelsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.DifficultyLevels
                .Select(d => new DifficultyLevelDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Value = d.Value
                });

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new DataTableResponse<DifficultyLevelDto>
            {
                Data = items,
                RecordsTotal = totalCount,
                RecordsFiltered = totalCount
            };
        }

        public async Task<DifficultyLevel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.DifficultyLevels.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<DifficultyLevel> AddAsync(DifficultyLevel difficultyLevel, CancellationToken cancellationToken = default)
        {
            await _context.DifficultyLevels.AddAsync(difficultyLevel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return difficultyLevel;
        }

        public async Task<DifficultyLevel> UpdateAsync(DifficultyLevel difficultyLevel, CancellationToken cancellationToken = default)
        {
            _context.DifficultyLevels.Update(difficultyLevel);
            await _context.SaveChangesAsync(cancellationToken);
            return difficultyLevel;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var difficultyLevel = await _context.DifficultyLevels.FindAsync(new object[] { id }, cancellationToken);
            if (difficultyLevel == null)
                return false;

            _context.DifficultyLevels.Remove(difficultyLevel);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
} 