using EnglishVocab.Domain.Entities;
using EnglishVocab.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IUserProgressRepository
    {
        Task<UserProgress> GetByIdAsync(int id);
        Task<UserProgress> GetByUserAndWordAsync(string userId, int wordId);
        Task<UserProgress> GetByUserIdAndWordIdAsync(string userId, int wordId);
        Task<IEnumerable<UserProgress>> GetByUserIdAsync(string userId);
        Task<IEnumerable<UserProgressWithWordInfo>> GetByUserIdWithWordsAsync(string userId);
        Task<IEnumerable<UserProgress>> GetDueForReviewAsync(string userId);
        Task<IEnumerable<UserProgress>> GetDueForReviewAsync(string userId, DateTime cutoffDate);
        Task<UserProgress> AddAsync(UserProgress userProgress);
        Task<UserProgress> UpdateAsync(UserProgress userProgress);
        Task<bool> DeleteAsync(int id);
        Task<int> CountWordsByMasteryLevelAsync(string userId, string masteryLevel);
        Task<int> CountTotalLearnedWordsAsync(string userId);
        Task<int> GetMasteredWordCountAsync(string userId);
        
        // Get recently studied words with progress
        Task<IEnumerable<UserProgressWithWordInfo>> GetRecentlyStudiedAsync(string userId, int count);
        Task<IEnumerable<UserProgressWithWordInfo>> GetRecentlyLearnedAsync(string userId, int count);
        Task<UserProgress> UpdateProgressAsync(string userId, int wordId, bool isKnown);
        Task<bool> ResetProgressForWordsAsync(string userId, IEnumerable<int> wordIds);
        Task<bool> ResetAllProgressAsync(string userId);
        Task<IEnumerable<UserProgress>> GetDueForReviewAsync(string userId, int count);
    }
} 