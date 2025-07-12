using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetUserProgressSummary;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetUserProgressSummary
{
    public class GetUserProgressSummaryQueryHandler : IRequestHandler<GetUserProgressSummaryQuery, UserProgressSummaryDto>
    {
        private readonly IUserProgressRepository _userProgressRepository;
        private readonly IUserStatisticsRepository _userStatisticsRepository;
        private readonly IMapper _mapper;

        public GetUserProgressSummaryQueryHandler(
            IUserProgressRepository userProgressRepository,
            IUserStatisticsRepository userStatisticsRepository,
            IMapper mapper)
        {
            _userProgressRepository = userProgressRepository;
            _userStatisticsRepository = userStatisticsRepository;
            _mapper = mapper;
        }

        public async Task<UserProgressSummaryDto> Handle(GetUserProgressSummaryQuery request, CancellationToken cancellationToken)
        {
            var statistics = await _userStatisticsRepository.GetByUserIdAsync(request.UserId);
            var progressSummary = new UserProgressSummaryDto
            {
                TotalWordsStudied = statistics?.TotalWordsStudied ?? 0,
                TotalSessions = statistics?.TotalSessions ?? 0,
                TotalTimeSpent = statistics?.TotalTimeSpent ?? 0,
                MasteredWords = statistics?.MasteredWords ?? 0,
                LearningWords = statistics?.LearningWords ?? 0,
                StreakDays = statistics?.StreakDays ?? 0,
                LastStudyDate = statistics?.LastStudyDate
            };
            
            return progressSummary;
        }
    }
} 