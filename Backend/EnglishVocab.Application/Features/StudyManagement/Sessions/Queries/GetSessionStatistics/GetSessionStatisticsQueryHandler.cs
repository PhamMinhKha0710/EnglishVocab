using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.StudyManagement.Sessions.Queries.GetSessionStatistics
{
    public class GetSessionStatisticsQueryHandler : IRequestHandler<GetSessionStatisticsQuery, SessionStatisticsDto>
    {
        private readonly IStudySessionRepository _studySessionRepository;
        private readonly IWordSetRepository _wordSetRepository;
        private readonly IMapper _mapper;

        public GetSessionStatisticsQueryHandler(
            IStudySessionRepository studySessionRepository,
            IWordSetRepository wordSetRepository,
            IMapper mapper)
        {
            _studySessionRepository = studySessionRepository;
            _wordSetRepository = wordSetRepository;
            _mapper = mapper;
        }

        public async Task<SessionStatisticsDto> Handle(GetSessionStatisticsQuery request, CancellationToken cancellationToken)
        {
            var session = await _studySessionRepository.GetByIdWithDetailsAsync(request.SessionId);
            
            if (session == null)
            {
                return null;
            }
            
            // Get WordSet information using repository
            var wordSet = await _wordSetRepository.GetByIdAsync(session.WordSetId);
            int totalWords = wordSet?.WordCount ?? 0;
            
            var statistics = new SessionStatisticsDto
            {
                SessionId = session.Id,
                Time = FormatDuration(session.Duration),
                Known = session.WordsKnown,
                NeedReview = session.WordsUnknown,
                Progress = CalculateProgress(session.WordsStudied, totalWords),
                TotalWords = totalWords,
                CompletedWords = session.WordsStudied
            };
            
            return statistics;
        }
        
        private string FormatDuration(TimeSpan duration)
        {
            if (duration.TotalHours >= 1)
            {
                return $"{Math.Floor(duration.TotalHours)}h {duration.Minutes}m";
            }
            else if (duration.TotalMinutes >= 1)
            {
                return $"{duration.Minutes}m {duration.Seconds}s";
            }
            else
            {
                return $"{duration.Seconds}s";
            }
        }
        
        private string CalculateProgress(int completed, int total)
        {
            if (total == 0) return "0%";
            
            double percentage = (double)completed / total * 100;
            return $"{Math.Round(percentage, 1)}%";
        }
    }
} 