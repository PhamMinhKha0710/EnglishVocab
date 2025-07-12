using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Entities;
using EnglishVocab.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishVocab.Infrastructure.Repositories
{
    public class UserProgressRepository : IUserProgressRepository
    {
        private readonly EnglishVocabDatabaseContext _context;

        public UserProgressRepository(EnglishVocabDatabaseContext context)
        {
            _context = context;
        }

        public async Task<UserProgress> AddAsync(UserProgress userProgress)
        {
            userProgress.DateCreated = DateTime.UtcNow;
            userProgress.DateModified = DateTime.UtcNow;
            await _context.UserProgresses.AddAsync(userProgress);
            await _context.SaveChangesAsync();
            return userProgress;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var progress = await _context.UserProgresses.FindAsync(id);
            if (progress == null)
                return false;

            _context.UserProgresses.Remove(progress);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UserProgress>> GetAllAsync()
        {
            return await _context.UserProgresses.ToListAsync();
        }

        public async Task<UserProgress> GetByIdAsync(int id)
        {
            return await _context.UserProgresses.FindAsync(id);
        }

        public async Task<UserProgress> GetByUserIdAndWordIdAsync(string userId, int wordId)
        {
            return await _context.UserProgresses
                .FirstOrDefaultAsync(up => up.UserId == userId && up.WordId == wordId);
        }

        public async Task<UserProgress> GetByUserAndWordAsync(string userId, int wordId)
        {
            return await _context.UserProgresses
                .FirstOrDefaultAsync(up => up.UserId == userId && up.WordId == wordId);
        }

        public async Task<IEnumerable<UserProgress>> GetByUserIdAsync(string userId)
        {
            var progressList = await _context.UserProgresses
                .Where(up => up.UserId == userId)
                .ToListAsync();
                
            return progressList;
        }
        
        public async Task<IEnumerable<UserProgressWithWordInfo>> GetByUserIdWithWordsAsync(string userId)
        {
            var query = from up in _context.UserProgresses
                        join w in _context.Words on up.WordId equals w.Id
                        where up.UserId == userId
                        select new UserProgressWithWordInfo
                        {
                            Progress = up,
                            WordText = w.English,
                            WordTranslation = w.Vietnamese
                        };
                        
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<UserProgress>> GetDueForReviewAsync(string userId, DateTime cutoffDate)
        {
            return await _context.UserProgresses
                .Where(up => up.UserId == userId && 
                            up.NextReviewDate <= cutoffDate && 
                            up.MasteryLevel < EnglishVocab.Constants.Constant.MasteryLevel.Mastered)
                .OrderBy(up => up.NextReviewDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserProgress>> GetDueForReviewAsync(string userId)
        {
            return await GetDueForReviewAsync(userId, DateTime.UtcNow);
        }

        public async Task<IEnumerable<UserProgress>> GetDueForReviewAsync(string userId, int count)
        {
            return await _context.UserProgresses
                .Where(up => up.UserId == userId && 
                           up.NextReviewDate <= DateTime.UtcNow && 
                           up.MasteryLevel < MasteryLevel.Mastered)
                .OrderBy(up => up.NextReviewDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<int> GetMasteredWordCountAsync(string userId)
        {
            return await _context.UserProgresses
                .CountAsync(up => up.UserId == userId && 
                            up.MasteryLevel == EnglishVocab.Constants.Constant.MasteryLevel.Mastered);
        }

        public async Task<IEnumerable<UserProgressWithWordInfo>> GetRecentlyLearnedAsync(string userId, int count)
        {
            var query = from up in _context.UserProgresses
                        join w in _context.Words on up.WordId equals w.Id
                        where up.UserId == userId
                        orderby up.LastReviewed descending
                        select new UserProgressWithWordInfo
                        {
                            Progress = up,
                            WordText = w.English,
                            WordTranslation = w.Vietnamese
                        };
                        
            return await query.Take(count).ToListAsync();
        }

        public async Task<IEnumerable<UserProgressWithWordInfo>> GetRecentlyStudiedAsync(string userId, int count)
        {
            var query = from up in _context.UserProgresses
                        join w in _context.Words on up.WordId equals w.Id
                        where up.UserId == userId
                        orderby up.LastReviewed descending
                        select new UserProgressWithWordInfo
                        {
                            Progress = up,
                            WordText = w.English,
                            WordTranslation = w.Vietnamese
                        };
                        
            return await query.Take(count).ToListAsync();
        }

        public async Task<UserProgress> UpdateAsync(UserProgress userProgress)
        {
            var existingProgress = await _context.UserProgresses.FindAsync(userProgress.Id);
            if (existingProgress == null)
                return null;

            existingProgress.MasteryLevel = userProgress.MasteryLevel;
            existingProgress.LastReviewed = userProgress.LastReviewed;
            existingProgress.NextReviewDate = userProgress.NextReviewDate;
            existingProgress.ReviewCount = userProgress.ReviewCount;
            existingProgress.CorrectCount = userProgress.CorrectCount;
            existingProgress.DateModified = DateTime.UtcNow;

            _context.UserProgresses.Update(existingProgress);
            await _context.SaveChangesAsync();
            return existingProgress;
        }

        public async Task<UserProgress> UpdateProgressAsync(string userId, int wordId, bool isKnown)
        {
            var progress = await GetByUserIdAndWordIdAsync(userId, wordId);
            
            if (progress == null)
            {
                // Create new progress record if it doesn't exist
                progress = new UserProgress
                {
                    UserId = userId,
                    WordId = wordId,
                    MasteryLevel = MasteryLevel.New,
                    ReviewCount = 1,
                    CorrectCount = isKnown ? 1 : 0,
                    LastReviewed = DateTime.UtcNow,
                    NextReviewDate = DateTime.UtcNow.AddDays(isKnown ? 1 : 0.5),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };
                
                return await AddAsync(progress);
            }
            
            // Update existing progress
            progress.LastReviewed = DateTime.UtcNow;
            progress.ReviewCount++;
            
            if (isKnown)
            {
                progress.CorrectCount++;
                
                // Update mastery level based on correct answers ratio
                double correctRatio = (double)progress.CorrectCount / progress.ReviewCount;
                
                if (correctRatio >= 0.9 && progress.ReviewCount >= 5)
                {
                    // Level up if performance is good
                    if (progress.MasteryLevel < MasteryLevel.Mastered)
                    {
                        progress.MasteryLevel++;
                    }
                    
                    // Calculate next review date based on spaced repetition
                    int daysToAdd = progress.MasteryLevel switch
                    {
                        MasteryLevel.New => 1,
                        MasteryLevel.Learning => 3,
                        MasteryLevel.Familiar => 7,
                        MasteryLevel.Known => 14,
                        MasteryLevel.Mastered => 30,
                        _ => 1
                    };
                    
                    progress.NextReviewDate = DateTime.UtcNow.AddDays(daysToAdd);
                }
                else
                {
                    // More modest progression
                    progress.NextReviewDate = DateTime.UtcNow.AddDays(1);
                }
            }
            else
            {
                // Incorrect answer - review sooner
                progress.NextReviewDate = DateTime.UtcNow.AddHours(12);
                
                // Possibly decrease mastery level if consistently wrong
                if (progress.MasteryLevel > MasteryLevel.New && 
                    ((double)progress.CorrectCount / progress.ReviewCount < 0.6) && 
                    progress.ReviewCount >= 3)
                {
                    progress.MasteryLevel--;
                }
            }
            
            progress.DateModified = DateTime.UtcNow;
            _context.UserProgresses.Update(progress);
            await _context.SaveChangesAsync();
            
            return progress;
        }

        public async Task<bool> ResetProgressForWordsAsync(string userId, IEnumerable<int> wordIds)
        {
            var progressEntries = await _context.UserProgresses
                .Where(up => up.UserId == userId && wordIds.Contains(up.WordId))
                .ToListAsync();
            
            if (!progressEntries.Any())
                return false;
            
            foreach (var progress in progressEntries)
            {
                progress.MasteryLevel = MasteryLevel.New;
                progress.ReviewCount = 0;
                progress.CorrectCount = 0;
                progress.LastReviewed = DateTime.UtcNow;
                progress.NextReviewDate = DateTime.UtcNow;
                progress.DateModified = DateTime.UtcNow;
            }
            
            _context.UserProgresses.UpdateRange(progressEntries);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> ResetAllProgressAsync(string userId)
        {
            var progressEntries = await _context.UserProgresses
                .Where(up => up.UserId == userId)
                .ToListAsync();
                
            if (!progressEntries.Any())
                return false;
                
            foreach (var progress in progressEntries)
            {
                progress.MasteryLevel = MasteryLevel.New;
                progress.ReviewCount = 0;
                progress.CorrectCount = 0;
                progress.LastReviewed = DateTime.UtcNow;
                progress.NextReviewDate = DateTime.UtcNow;
                progress.DateModified = DateTime.UtcNow;
            }
            
            _context.UserProgresses.UpdateRange(progressEntries);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<int> CountWordsByMasteryLevelAsync(string userId, string masteryLevel)
        {
            var level = Enum.Parse<MasteryLevel>(masteryLevel);
            return await _context.UserProgresses
                .CountAsync(up => up.UserId == userId && up.MasteryLevel == level);
        }
        
        public async Task<int> CountTotalLearnedWordsAsync(string userId)
        {
            return await _context.UserProgresses
                .CountAsync(up => up.UserId == userId && up.MasteryLevel > MasteryLevel.New);
        }
    }
} 
 