using MediatR;

namespace EnglishVocab.Application.Features.StudySessions.Commands.EndSession
{
    public class EndSessionCommand : IRequest<bool>
    {
        public int SessionId { get; set; }
        public int PointsEarned { get; set; }
    }
} 