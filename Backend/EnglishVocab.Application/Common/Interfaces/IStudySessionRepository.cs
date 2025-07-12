using EnglishVocab.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishVocab.Constants.Constant;
using EnglishVocab.Application.Features.StudyManagement.DTOs;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IStudySessionRepository
    {
        Task<StudySession> GetByIdAsync(int id);
        Task<StudySession> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<StudySession>> GetByUserIdAsync(string userId);
        Task<StudySession> GetActiveSessionByUserIdAsync(string userId);
        Task<StudySession> GetCurrentSessionForUserAsync(string userId);
        Task<IEnumerable<StudySession>> GetCompletedSessionsByUserIdAsync(string userId);
        Task<StudySession> StartSessionAsync(StudySession session);
        Task<StudySession> UpdateSessionAsync(StudySession session);
        Task<StudySession> AddAsync(StudySession session);
        Task<StudySession> UpdateAsync(StudySession session);
        Task<StudySession> EndSessionAsync(int sessionId);
        Task<StudySession> PauseSessionAsync(int sessionId);
        Task<StudySession> ResumeSessionAsync(int sessionId);
        Task<bool> DeleteSessionAsync(int sessionId);
        Task<IEnumerable<StudySession>> GetRecentSessionsAsync(string userId, int count);
        
        // Missing methods
        Task<StudySession> CreateSessionAsync(StudySession session);
        Task<int> CreateSessionAsync(string userId, int wordSetId, StudyMode studyMode, bool shuffleWords);
        Task<StudySession> CompleteSessionAsync(int sessionId, int pointsEarned);
        Task<Word> GetNextWordForSessionAsync(int sessionId);
        Task<bool> MarkWordAsync(int sessionId, int wordId, bool isKnown, int responseTimeMs);
        
        // Daily progress method
        Task<IEnumerable<DailyProgressDto>> GetDailyProgressAsync(string userId, int days);
    }
} 