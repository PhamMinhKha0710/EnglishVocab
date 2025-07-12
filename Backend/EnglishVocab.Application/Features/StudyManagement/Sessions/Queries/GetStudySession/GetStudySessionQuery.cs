using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.StudySessions.Queries.GetStudySession
{
    public class GetStudySessionQuery : IRequest<StudySessionDto>
    {
        public int SessionId { get; set; }
    }
} 