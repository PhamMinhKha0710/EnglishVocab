using EnglishVocab.Application.Features.StudyManagement.DTOs;
using EnglishVocab.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IStudyService
    {
        Task<StudySession> StartSessionAsync(string userId, int wordSetId, Constants.Constant.StudyMode studyMode, bool shuffleWords, CancellationToken cancellationToken = default);
        Task<StudySession> EndSessionAsync(int sessionId, CancellationToken cancellationToken = default);
        Task<StudySession> PauseSessionAsync(int sessionId, CancellationToken cancellationToken = default);
        Task<StudySession> ResumeSessionAsync(int sessionId, CancellationToken cancellationToken = default);
        Task<Word> GetNextWordForSessionAsync(int sessionId, CancellationToken cancellationToken = default);
        Task<bool> MarkWordAsync(int sessionId, int wordId, bool isKnown, int responseTimeMs = 0, CancellationToken cancellationToken = default);
        Task<StudySession> GetSessionByIdAsync(int sessionId, CancellationToken cancellationToken = default);
        Task<IEnumerable<StudySession>> GetUserSessionsAsync(string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<DailyProgressDto>> GetDailyProgressAsync(string userId, int days = 30, CancellationToken cancellationToken = default);
        Task<StudySession> GetActiveSessionByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    }
} 
 