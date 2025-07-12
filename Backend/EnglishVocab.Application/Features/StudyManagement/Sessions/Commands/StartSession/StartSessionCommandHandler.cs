using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.StudySessions.Commands.StartSession
{
    public class StartSessionCommandHandler : IRequestHandler<StartSessionCommand, int>
    {
        private readonly IStudySessionRepository _studySessionRepository;
        private readonly IWordSetRepository _wordSetRepository;
        private readonly IMapper _mapper;

        public StartSessionCommandHandler(
            IStudySessionRepository studySessionRepository,
            IWordSetRepository wordSetRepository,
            IMapper mapper)
        {
            _studySessionRepository = studySessionRepository;
            _wordSetRepository = wordSetRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(StartSessionCommand request, CancellationToken cancellationToken)
        {
            var wordSet = await _wordSetRepository.GetByIdAsync(request.WordSetId);
            
            if (wordSet == null)
            {
                throw new ApplicationException($"Word set with ID {request.WordSetId} not found");
            }
            
            var sessionId = await _studySessionRepository.CreateSessionAsync(
                request.UserId,
                request.WordSetId,
                request.StudyMode,
                request.ShuffleWords);
                
            return sessionId;
        }
    }
} 