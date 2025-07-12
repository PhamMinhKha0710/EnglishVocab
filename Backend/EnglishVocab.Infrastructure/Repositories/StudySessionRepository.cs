using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Domain.Entities;
using EnglishVocab.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishVocab.Constants.Constant;
using EnglishVocab.Application.Features.StudyManagement.DTOs;

namespace EnglishVocab.Infrastructure.Repositories
{
    public class StudySessionRepository : IStudySessionRepository
    {
        private readonly EnglishVocabDatabaseContext _context;

        public StudySessionRepository(EnglishVocabDatabaseContext context)
        {
            _context = context;
        }

        public async Task<StudySession> AddAsync(StudySession studySession)
        {
            studySession.DateCreated = DateTime.UtcNow;
            studySession.DateModified = DateTime.UtcNow;
            await _context.StudySessions.AddAsync(studySession);
            await _context.SaveChangesAsync();
            return studySession;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var studySession = await _context.StudySessions.FindAsync(id);
            if (studySession == null)
                return false;

            _context.StudySessions.Remove(studySession);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<StudySession>> GetAllAsync()
        {
            return await _context.StudySessions.ToListAsync();
        }

        public async Task<StudySession> GetByIdAsync(int id)
        {
            return await _context.StudySessions.FindAsync(id);
        }

        public async Task<StudySession> GetByIdWithDetailsAsync(int id)
        {
            return await _context.StudySessions.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<StudySession>> GetByUserIdAsync(string userId)
        {
            return await _context.StudySessions
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<StudySession> GetCurrentSessionForUserAsync(string userId)
        {
            return await _context.StudySessions
                .Where(s => s.UserId == userId && s.EndTime == null)
                .OrderByDescending(s => s.StartTime)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<StudySession>> GetRecentSessionsAsync(string userId, int count)
        {
            return await _context.StudySessions
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.StartTime)
                .Take(count)
                .ToListAsync();
        }

        public async Task<StudySession> UpdateAsync(StudySession studySession)
        {
            var existingSession = await _context.StudySessions.FindAsync(studySession.Id);
            if (existingSession == null)
                return null;

            existingSession.EndTime = studySession.EndTime;
            existingSession.WordsStudied = studySession.WordsStudied;
            existingSession.WordsKnown = studySession.WordsKnown;
            existingSession.WordsUnknown = studySession.WordsUnknown;
            existingSession.WordsSkipped = studySession.WordsSkipped;
            existingSession.PointsEarned = studySession.PointsEarned;
            existingSession.Status = studySession.Status;
            existingSession.DateModified = DateTime.UtcNow;

            _context.StudySessions.Update(existingSession);
            await _context.SaveChangesAsync();
            return existingSession;
        }
        
        public async Task<StudySession> GetActiveSessionByUserIdAsync(string userId)
        {
            return await _context.StudySessions
                .Where(s => s.UserId == userId && s.EndTime == null)
                .OrderByDescending(s => s.StartTime)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<StudySession>> GetCompletedSessionsByUserIdAsync(string userId)
        {
            return await _context.StudySessions
                .Where(s => s.UserId == userId && s.EndTime != null)
                .OrderByDescending(s => s.EndTime)
                .ToListAsync();
        }

        public async Task<StudySession> StartSessionAsync(StudySession session)
        {
            session.StartTime = DateTime.UtcNow;
            session.Status = "active";
            return await AddAsync(session);
        }

        public async Task<StudySession> UpdateSessionAsync(StudySession session)
        {
            return await UpdateAsync(session);
        }

        public async Task<StudySession> EndSessionAsync(int id)
        {
            var session = await GetByIdAsync(id);
            if (session == null)
                return null;

            session.EndTime = DateTime.UtcNow;
            session.Status = "complete";
            await UpdateAsync(session);
            return session;
        }

        public async Task<StudySession> PauseSessionAsync(int id)
        {
            var session = await GetByIdAsync(id);
            if (session == null)
                return null;

            session.Status = "paused";
            await UpdateAsync(session);
            return session;
        }

        public async Task<StudySession> ResumeSessionAsync(int id)
        {
            var session = await GetByIdAsync(id);
            if (session == null)
                return null;

            session.Status = "active";
            await UpdateAsync(session);
            return session;
        }

        public async Task<bool> DeleteSessionAsync(int id)
        {
            return await DeleteAsync(id);
        }
        
        public async Task<StudySession> CreateSessionAsync(StudySession session)
        {
            session.StartTime = DateTime.UtcNow;
            session.Status = "active";
            session.DateCreated = DateTime.UtcNow;
            session.DateModified = DateTime.UtcNow;
            await _context.StudySessions.AddAsync(session);
            await _context.SaveChangesAsync();
            return session;
        }
        
        public async Task<int> CreateSessionAsync(string userId, int wordSetId, StudyMode studyMode, bool shuffleWords)
        {
            var session = new StudySession
            {
                UserId = userId,
                WordSetId = wordSetId,
                StudyMode = studyMode,
                ShuffleWords = shuffleWords,
                StartTime = DateTime.UtcNow,
                Status = "active",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                WordsStudied = 0,
                WordsKnown = 0,
                WordsUnknown = 0,
                WordsSkipped = 0,
                PointsEarned = 0
            };
            
            await _context.StudySessions.AddAsync(session);
            await _context.SaveChangesAsync();
            return session.Id;
        }
        
        public async Task<StudySession> CompleteSessionAsync(int sessionId, int pointsEarned)
        {
            var session = await GetByIdAsync(sessionId);
            if (session == null)
                return null;

            session.EndTime = DateTime.UtcNow;
            session.Status = "complete";
            session.PointsEarned = pointsEarned;
            
            _context.StudySessions.Update(session);
            await _context.SaveChangesAsync();
            return session;
        }
        
        public async Task<Word> GetNextWordForSessionAsync(int sessionId)
        {
            var session = await _context.StudySessions.FindAsync(sessionId);
                
            if (session == null)
                return null;
                
            // Lấy danh sách từ trong wordset
            var wordIds = await _context.WordSetWords
                .Where(wsw => wsw.WordSetId == session.WordSetId)
                .Select(wsw => wsw.WordId)
                .ToListAsync();
                
            if (!wordIds.Any())
                return null;
                
            // Lấy từ đầu tiên trong danh sách
            // Trong thực tế, bạn nên triển khai thuật toán spaced repetition ở đây
            var word = await _context.Words
                .FirstOrDefaultAsync(w => wordIds.Contains(w.Id));
                
            return word;
        }
        
        public async Task<bool> MarkWordAsync(int sessionId, int wordId, bool isKnown, int responseTimeMs)
        {
            var session = await GetByIdAsync(sessionId);
            if (session == null)
                return false;
                
            // Update session stats
            if (isKnown)
                session.WordsKnown = session.WordsKnown + 1;
            else
                session.WordsUnknown = session.WordsUnknown + 1;
                
            session.WordsStudied = session.WordsStudied + 1;
            
            _context.StudySessions.Update(session);
            
            // Ở đây bạn có thể cập nhật tiến độ học từ của người dùng
            // Điều này sẽ yêu cầu tiêm IUserProgressRepository
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DailyProgressDto>> GetDailyProgressAsync(string userId, int days)
        {
            var endDate = DateTime.UtcNow.Date;
            var startDate = endDate.AddDays(-days + 1);
            
            // Get all sessions within the date range
            var sessions = await _context.StudySessions
                .Where(s => s.UserId == userId && 
                       s.StartTime >= startDate && 
                       s.StartTime <= endDate.AddDays(1).AddTicks(-1) && 
                       s.EndTime != null)
                .ToListAsync();
            
            // Group sessions by date and calculate statistics
            var dailyProgress = sessions
                .GroupBy(s => s.StartTime.Date)
                .Select(g => new DailyProgressDto
                {
                    Date = g.Key,
                    WordsStudied = g.Sum(s => s.WordsStudied),
                    TotalMinutes = g.Sum(s => s.EndTime.HasValue ? 
                        (int)Math.Ceiling((s.EndTime.Value - s.StartTime).TotalMinutes) : 0)
                })
                .OrderBy(dp => dp.Date)
                .ToList();
            
            // Fill in missing dates with zero values
            var result = new List<DailyProgressDto>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var existingProgress = dailyProgress.FirstOrDefault(dp => dp.Date.Date == date.Date);
                if (existingProgress != null)
                {
                    result.Add(existingProgress);
                }
                else
                {
                    result.Add(new DailyProgressDto
                    {
                        Date = date,
                        WordsStudied = 0,
                        TotalMinutes = 0
                    });
                }
            }
            
            return result;
        }
    }
} 
 