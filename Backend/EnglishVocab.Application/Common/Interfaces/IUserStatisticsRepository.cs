using EnglishVocab.Domain.Entities;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IUserStatisticsRepository
    {
        Task<UserStatistics> GetByUserIdAsync(string userId);
        Task<UserStatistics> UpdateAsync(UserStatistics userStatistics);
        Task<UserStatistics> AddAsync(UserStatistics userStatistics);
        Task<bool> UpdateStreakAsync(string userId);
        Task<bool> AddPointsAsync(string userId, int points);
        Task<bool> IncrementWordsLearnedAsync(string userId, int count = 1);
        Task<bool> UpdateLastActivityAsync(string userId);
    }
} 