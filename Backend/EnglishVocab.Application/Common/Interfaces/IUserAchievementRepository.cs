using EnglishVocab.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IUserAchievementRepository
    {
        Task<UserAchievement> GetByIdAsync(int id);
        Task<IEnumerable<UserAchievement>> GetByUserIdAsync(string userId);
        Task<IEnumerable<UserAchievement>> GetNewByUserIdAsync(string userId);
        Task<UserAchievement> AddAsync(UserAchievement achievement);
        Task<UserAchievement> UpdateAsync(UserAchievement achievement);
        Task<bool> MarkAsSeenAsync(int id);
        Task<bool> MarkAllAsSeenAsync(string userId);
        Task<bool> HasAchievementAsync(string userId, string achievementType, int value);
    }
} 
 