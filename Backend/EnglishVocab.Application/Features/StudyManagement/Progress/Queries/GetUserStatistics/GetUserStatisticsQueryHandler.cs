using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetUserStatistics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetUserStatistics
{
    public class GetUserStatisticsQueryHandler : IRequestHandler<GetUserStatisticsQuery, UserStatisticsDto>
    {
        private readonly IUserProgressRepository _userProgressRepository;
        private readonly IStudySessionRepository _studySessionRepository;
        private readonly IUserStatisticsRepository _userStatisticsRepository;

        public GetUserStatisticsQueryHandler(
            IUserProgressRepository userProgressRepository,
            IStudySessionRepository studySessionRepository,
            IUserStatisticsRepository userStatisticsRepository)
        {
            _userProgressRepository = userProgressRepository;
            _studySessionRepository = studySessionRepository;
            _userStatisticsRepository = userStatisticsRepository;
        }

        public async Task<UserStatisticsDto> Handle(GetUserStatisticsQuery request, CancellationToken cancellationToken)
        {
            var userStats = await _userStatisticsRepository.GetByUserIdAsync(request.UserId);
            
            if (userStats == null)
            {
                return new UserStatisticsDto
                {
                    TotalWordsStudied = 0,
                    MasteredWords = 0,
                    LearningWords = 0,
                    TotalSessions = 0,
                    TotalTimeSpent = 0,
                    StreakDays = 0,
                    LastStudyDate = null,
                    DailyProgress = new List<DailyProgressDto>()
                };
            }

            // Lấy thông tin từ các repository khác để bổ sung
            var totalLearned = await _userProgressRepository.CountTotalLearnedWordsAsync(request.UserId);
            var masteredWords = await _userProgressRepository.CountWordsByMasteryLevelAsync(request.UserId, "Mastered");
            var learningWords = await _userProgressRepository.CountWordsByMasteryLevelAsync(request.UserId, "Learning");

            // Lấy dữ liệu tiến độ hàng ngày (7 ngày gần nhất)
            var dailyProgress = await _studySessionRepository.GetDailyProgressAsync(request.UserId, 7);
            var dailyProgressDtos = dailyProgress.Select(dp => new DailyProgressDto
            {
                Date = dp.Date,
                WordsStudied = dp.WordsStudied,
                TotalMinutes = dp.TotalMinutes
            }).ToList();

            return new UserStatisticsDto
            {
                TotalWordsStudied = totalLearned,
                MasteredWords = masteredWords,
                LearningWords = learningWords,
                TotalSessions = userStats.TotalSessions,
                TotalTimeSpent = userStats.TotalTimeSpentMinutes,
                StreakDays = userStats.CurrentStreak,
                LastStudyDate = userStats.LastActivity,
                DailyProgress = dailyProgressDtos
            };
        }
    }
} 