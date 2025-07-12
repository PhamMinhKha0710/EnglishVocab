using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using EnglishVocab.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IDifficultyLevelRepository
    {
        Task<IEnumerable<DifficultyLevelDto>> GetAllDifficultyLevelsAsync(CancellationToken cancellationToken = default);
        Task<DataTableResponse<DifficultyLevelDto>> GetPaginatedDifficultyLevelsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<DifficultyLevel> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<DifficultyLevel> AddAsync(DifficultyLevel difficultyLevel, CancellationToken cancellationToken = default);
        Task<DifficultyLevel> UpdateAsync(DifficultyLevel difficultyLevel, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
} 