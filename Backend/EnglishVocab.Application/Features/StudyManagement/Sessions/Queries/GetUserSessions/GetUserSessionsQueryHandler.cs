using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.StudySessions.Queries.GetUserSessions
{
    public class GetUserSessionsQueryHandler : IRequestHandler<GetUserSessionsQuery, IEnumerable<StudySessionDto>>
    {
        private readonly IStudySessionRepository _studySessionRepository;
        private readonly IMapper _mapper;

        public GetUserSessionsQueryHandler(IStudySessionRepository studySessionRepository, IMapper mapper)
        {
            _studySessionRepository = studySessionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudySessionDto>> Handle(GetUserSessionsQuery request, CancellationToken cancellationToken)
        {
            var sessions = await _studySessionRepository.GetByUserIdAsync(request.UserId);
            
            if (!request.IncludeCompleted)
            {
                sessions = sessions.Where(s => s.Status != "completed");
            }
            
            return _mapper.Map<IEnumerable<StudySessionDto>>(sessions);
        }
    }
} 