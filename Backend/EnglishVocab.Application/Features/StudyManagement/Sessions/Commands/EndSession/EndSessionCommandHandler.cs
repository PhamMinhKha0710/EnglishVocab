using EnglishVocab.Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.StudySessions.Commands.EndSession
{
    public class EndSessionCommandHandler : IRequestHandler<EndSessionCommand, bool>
    {
        private readonly IStudySessionRepository _studySessionRepository;

        public EndSessionCommandHandler(IStudySessionRepository studySessionRepository)
        {
            _studySessionRepository = studySessionRepository;
        }

        public async Task<bool> Handle(EndSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _studySessionRepository.GetByIdAsync(request.SessionId);
            
            if (session == null)
            {
                throw new ApplicationException($"Study session with ID {request.SessionId} not found");
            }
            
            if (session.Status == "completed")
            {
                return true; // Session already completed
            }
            
            var result = await _studySessionRepository.CompleteSessionAsync(request.SessionId, request.PointsEarned);
            return result != null; // Return true if session was successfully completed
        }
    }
} 