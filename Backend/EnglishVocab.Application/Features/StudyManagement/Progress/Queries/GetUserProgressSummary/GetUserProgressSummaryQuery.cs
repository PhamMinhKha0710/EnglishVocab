using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetUserProgressSummary
{
    public class GetUserProgressSummaryQuery : IRequest<UserProgressSummaryDto>
    {
        public string UserId { get; set; }
    }
} 