using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.StudySessions.Queries.GetStudySession
{
    public class GetStudySessionQueryHandler : IRequestHandler<GetStudySessionQuery, StudySessionDto>
    {
        private readonly IStudySessionRepository _studySessionRepository;
        private readonly IWordSetRepository _wordSetRepository;
        private readonly IMapper _mapper;

        public GetStudySessionQueryHandler(
            IStudySessionRepository studySessionRepository,
            IWordSetRepository wordSetRepository,
            IMapper mapper)
        {
            _studySessionRepository = studySessionRepository;
            _wordSetRepository = wordSetRepository;
            _mapper = mapper;
        }

        public async Task<StudySessionDto> Handle(GetStudySessionQuery request, CancellationToken cancellationToken)
        {
            var session = await _studySessionRepository.GetByIdWithDetailsAsync(request.SessionId);
            if (session == null) return null;
            
            var sessionDto = _mapper.Map<StudySessionDto>(session);
            
            // Get WordSet name separately since navigation property was removed
            var wordSet = await _wordSetRepository.GetByIdAsync(session.WordSetId);
            sessionDto.WordSetName = wordSet?.Name ?? "Unknown Set";
            
            return sessionDto;
        }
    }
} 