using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Domain.Entities;
using EnglishVocab.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EnglishVocab.Infrastructure.Repositories
{
    public class UserStatisticsRepository : IUserStatisticsRepository
    {
        private readonly EnglishVocabDatabaseContext _context;

        public UserStatisticsRepository(EnglishVocabDatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPointsAsync(string userId, int points)
        {
            var stats = await GetByUserIdAsync(userId);
            if (stats == null)
            {
                stats = await CreateAsync(new UserStatistics
                {
                    UserId = userId,
                    TotalPoints = points
                });
                return stats != null;
            }

            stats.TotalPoints += points;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserStatistics> CreateAsync(UserStatistics statistics)
        {
            statistics.LastActivity = DateTime.UtcNow;
            statistics.StreakUpdatedAt = DateTime.UtcNow;
            await _context.UserStatistics.AddAsync(statistics);
            await _context.SaveChangesAsync();
            return statistics;
        }
        
        public async Task<UserStatistics> AddAsync(UserStatistics userStatistics)
        {
            // Reuse the existing CreateAsync method
            return await CreateAsync(userStatistics);
        }

        public async Task<UserStatistics> GetByUserIdAsync(string userId)
        {
            return await _context.UserStatistics
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<bool> IncrementWordsLearnedAsync(string userId, int count = 1)
        {
            var stats = await GetByUserIdAsync(userId);
            if (stats == null)
            {
                stats = await CreateAsync(new UserStatistics
                {
                    UserId = userId,
                    TotalWordsLearned = count
                });
                return stats != null;
            }

            stats.TotalWordsLearned += count;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLastActivityAsync(string userId)
        {
            var stats = await GetByUserIdAsync(userId);
            if (stats == null)
            {
                stats = await CreateAsync(new UserStatistics
                {
                    UserId = userId
                });
                return stats != null;
            }

            stats.LastActivity = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserStatistics> UpdateAsync(UserStatistics statistics)
        {
            var existingStats = await _context.UserStatistics
                .FirstOrDefaultAsync(s => s.Id == statistics.Id);

            if (existingStats == null)
                return null;

            existingStats.TotalPoints = statistics.TotalPoints;
            existingStats.CurrentStreak = statistics.CurrentStreak;
            existingStats.LongestStreak = statistics.LongestStreak;
            existingStats.TotalWordsLearned = statistics.TotalWordsLearned;
            existingStats.TotalSessions = statistics.TotalSessions;
            existingStats.TotalTimeSpentMinutes = statistics.TotalTimeSpentMinutes;
            existingStats.LastActivity = statistics.LastActivity;
            existingStats.StreakUpdatedAt = statistics.StreakUpdatedAt;

            await _context.SaveChangesAsync();
            return existingStats;
        }

        public async Task<bool> UpdateStreakAsync(string userId)
        {
            var stats = await GetByUserIdAsync(userId);
            if (stats == null)
            {
                stats = await CreateAsync(new UserStatistics
                {
                    UserId = userId,
                    CurrentStreak = 1,
                    LongestStreak = 1
                });
                return stats != null;
            }

            // Kiểm tra xem đã cập nhật streak trong ngày chưa
            var today = DateTime.UtcNow.Date;
            var lastUpdate = stats.StreakUpdatedAt.Date;

            if (today == lastUpdate)
            {
                // Đã cập nhật streak trong ngày, không làm gì
                return true;
            }
            else if (today.AddDays(-1) == lastUpdate)
            {
                // Ngày liên tiếp, tăng streak
                stats.CurrentStreak++;
                if (stats.CurrentStreak > stats.LongestStreak)
                {
                    stats.LongestStreak = stats.CurrentStreak;
                }
            }
            else
            {
                // Không phải ngày liên tiếp, reset streak
                stats.CurrentStreak = 1;
            }

            stats.StreakUpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 