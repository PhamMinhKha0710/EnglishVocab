using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Services
{
    public class StudyService : IStudyService
    {
        private readonly IStudySessionRepository _studySessionRepository;
        private readonly IUserProgressRepository _userProgressRepository;
        private readonly IWordRepository _wordRepository;
        private readonly ISpacedRepetitionService _spacedRepetitionService;

        public StudyService(
            IStudySessionRepository studySessionRepository,
            IUserProgressRepository userProgressRepository,
            IWordRepository wordRepository,
            ISpacedRepetitionService spacedRepetitionService)
        {
            _studySessionRepository = studySessionRepository;
            _userProgressRepository = userProgressRepository;
            _wordRepository = wordRepository;
            _spacedRepetitionService = spacedRepetitionService;
        }

        public async Task<StudySession> StartSessionAsync(string userId, int wordSetId, StudyMode studyMode, bool shuffleWords, CancellationToken cancellationToken = default)
        {
            // Kiểm tra xem có session đang active không
            var activeSession = await _studySessionRepository.GetActiveSessionByUserIdAsync(userId);
            if (activeSession != null)
            {
                // Kết thúc session cũ nếu có
                await EndSessionAsync(activeSession.Id, cancellationToken);
            }

            // Tạo session mới
            var session = new StudySession
            {
                UserId = userId,
                WordSetId = wordSetId,
                StartTime = DateTime.UtcNow,
                StudyMode = studyMode,
                ShuffleWords = shuffleWords,
                Status = "active"
            };

            return await _studySessionRepository.AddAsync(session);
        }

        public async Task<StudySession> EndSessionAsync(int sessionId, CancellationToken cancellationToken = default)
        {
            var session = await _studySessionRepository.GetByIdAsync(sessionId);
            if (session == null)
                throw new Exception($"Study session with ID {sessionId} not found");

            session.EndTime = DateTime.UtcNow;
            session.Status = "completed";

            return await _studySessionRepository.UpdateAsync(session);
        }

        public async Task<StudySession> PauseSessionAsync(int sessionId, CancellationToken cancellationToken = default)
        {
            var session = await _studySessionRepository.GetByIdAsync(sessionId);
            if (session == null)
                throw new Exception($"Study session with ID {sessionId} not found");

            session.Status = "paused";
            return await _studySessionRepository.UpdateAsync(session);
        }

        public async Task<StudySession> ResumeSessionAsync(int sessionId, CancellationToken cancellationToken = default)
        {
            var session = await _studySessionRepository.GetByIdAsync(sessionId);
            if (session == null)
                throw new Exception($"Study session with ID {sessionId} not found");

            session.Status = "active";
            return await _studySessionRepository.UpdateAsync(session);
        }

        public async Task<Word> GetNextWordForSessionAsync(int sessionId, CancellationToken cancellationToken = default)
        {
            return await _studySessionRepository.GetNextWordForSessionAsync(sessionId);
        }

        public async Task<bool> MarkWordAsync(int sessionId, int wordId, bool isKnown, int responseTimeMs = 0, CancellationToken cancellationToken = default)
        {
            var session = await _studySessionRepository.GetByIdAsync(sessionId);
            if (session == null)
                throw new Exception($"Study session with ID {sessionId} not found");

            // Cập nhật session
            if (isKnown)
                session.WordsKnown++;
            else
                session.WordsUnknown++;

            session.WordsStudied++;
            await _studySessionRepository.UpdateAsync(session);

            // Cập nhật user progress
            var userProgress = await _userProgressRepository.GetByUserIdAndWordIdAsync(session.UserId, wordId);
            if (userProgress == null)
            {
                userProgress = new UserProgress
                {
                    UserId = session.UserId,
                    WordId = wordId,
                    MasteryLevel = MasteryLevel.New,
                    LastReviewed = DateTime.UtcNow,
                    NextReviewDate = DateTime.UtcNow.AddDays(1),
                    EaseFactorInPercentage = 250, // 2.5
                    IntervalInDays = 1
                };
                await _userProgressRepository.AddAsync(userProgress);
            }

            // Cập nhật thông tin học tập
            userProgress.LastReviewedAt = DateTime.UtcNow;
            userProgress.ReviewCount++;
            if (isKnown)
                userProgress.CorrectCount++;
            else
                userProgress.IncorrectCount++;

            // Tính toán thông tin spaced repetition
            var (newIntervalInDays, newEaseFactor, newMasteryLevel) = _spacedRepetitionService.CalculateNextReview(
                userProgress.EaseFactorInPercentage,
                userProgress.IntervalInDays,
                isKnown,
                userProgress.MasteryLevel);

            userProgress.EaseFactorInPercentage = newEaseFactor;
            userProgress.IntervalInDays = newIntervalInDays;
            userProgress.MasteryLevel = newMasteryLevel;
            userProgress.NextReviewDate = _spacedRepetitionService.CalculateNextReviewDate(
                DateTime.UtcNow, newIntervalInDays);

            await _userProgressRepository.UpdateAsync(userProgress);

            // Ghi lại hành động của người dùng
            var userAction = new UserAction
            {
                UserId = session.UserId,
                StudySessionId = sessionId,
                WordId = wordId,
                ActionType = isKnown ? "known" : "unknown",
                Timestamp = DateTime.UtcNow,
                ResponseTimeMs = responseTimeMs
            };

            // TODO: Lưu userAction vào database

            return true;
        }

        public async Task<StudySession> GetSessionByIdAsync(int sessionId, CancellationToken cancellationToken = default)
        {
            return await _studySessionRepository.GetByIdWithDetailsAsync(sessionId);
        }

        public async Task<IEnumerable<StudySession>> GetUserSessionsAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _studySessionRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<DailyProgressDto>> GetDailyProgressAsync(string userId, int days = 30, CancellationToken cancellationToken = default)
        {
            return await _studySessionRepository.GetDailyProgressAsync(userId, days);
        }

        public async Task<StudySession> GetActiveSessionByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _studySessionRepository.GetActiveSessionByUserIdAsync(userId);
        }
    }
} 
 