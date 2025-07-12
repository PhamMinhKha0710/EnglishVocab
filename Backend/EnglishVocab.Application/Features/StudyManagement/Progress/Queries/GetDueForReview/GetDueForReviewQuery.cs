using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetDueForReview
{
    public class GetDueForReviewQuery : IRequest<IEnumerable<WordProgressDto>>
    {
        public string UserId { get; set; }
        public int Count { get; set; } = 10;
    }
} 