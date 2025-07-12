using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetProgressByWord
{
    public class GetProgressByWordQuery : IRequest<WordProgressDto>
    {
        public string UserId { get; set; }
        public int WordId { get; set; }
    }
} 