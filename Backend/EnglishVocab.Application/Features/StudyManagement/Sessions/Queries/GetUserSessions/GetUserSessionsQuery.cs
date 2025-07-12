using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.StudySessions.Queries.GetUserSessions
{
    public class GetUserSessionsQuery : IRequest<IEnumerable<StudySessionDto>>
    {
        public string UserId { get; set; }
        public bool IncludeCompleted { get; set; } = false;
    }
} 