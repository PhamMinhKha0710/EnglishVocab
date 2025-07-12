using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.StudySessions.Queries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace EnglishVocab.Application.Features.StudySessions.Commands.MarkFlashcard
{
    public class MarkFlashcardCommandHandler : IRequestHandler<MarkFlashcardCommand, bool>
    {
        private readonly IStudySessionRepository _studySessionRepository;
        private readonly IUserProgressRepository _userProgressRepository;
        private readonly IStudyService _studyService;
        private readonly IWordService _wordService;
        private readonly IMapper _mapper;

        public MarkFlashcardCommandHandler(
            IStudySessionRepository studySessionRepository,
            IUserProgressRepository userProgressRepository,
            IStudyService studyService,
            IWordService wordService,
            IMapper mapper)
        {
            _studySessionRepository = studySessionRepository;
            _userProgressRepository = userProgressRepository;
            _studyService = studyService;
            _wordService = wordService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(MarkFlashcardCommand request, CancellationToken cancellationToken)
        {
            var session = await _studySessionRepository.GetByIdAsync(request.SessionId);
            
            if (session == null)
            {
                throw new ApplicationException($"Study session with ID {request.SessionId} not found");
            }
            
            if (session.Status == "completed")
            {
                return false; // Cannot mark flashcards in completed session
            }
            
            await _studySessionRepository.MarkWordAsync(
                request.SessionId,
                request.WordId,
                request.IsKnown,
                request.ResponseTimeMs);
                
            if (!string.IsNullOrEmpty(session.UserId))
            {
                await _userProgressRepository.UpdateProgressAsync(
                    session.UserId,
                    request.WordId,
                    request.IsKnown);
            }
                
            return true;
        }
    }
} 