
using EnglishVocab.Domain.Entities;


namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IDifficultyLevelRepository
    {
        Task<List<DifficultyLevel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IQueryable<DifficultyLevel>> GetAllDifficultyLevelsAsync(string searchTerm = null);
        Task<DifficultyLevel> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<DifficultyLevel> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<DifficultyLevel> AddAsync(DifficultyLevel difficultyLevel, CancellationToken cancellationToken = default);
        Task<DifficultyLevel> UpdateAsync(DifficultyLevel difficultyLevel, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
} 