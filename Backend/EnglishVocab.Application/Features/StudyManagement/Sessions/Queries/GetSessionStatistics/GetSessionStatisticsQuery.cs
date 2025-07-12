using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.StudyManagement.Sessions.Queries.GetSessionStatistics
{
    public class GetSessionStatisticsQuery : IRequest<SessionStatisticsDto>
    {
        public int SessionId { get; set; }
    }
} 