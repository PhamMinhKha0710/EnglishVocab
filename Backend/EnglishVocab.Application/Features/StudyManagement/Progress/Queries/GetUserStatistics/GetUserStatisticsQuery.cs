using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetUserStatistics
{
    public class GetUserStatisticsQuery : IRequest<UserStatisticsDto>
    {
        public string UserId { get; set; }
    }
} 