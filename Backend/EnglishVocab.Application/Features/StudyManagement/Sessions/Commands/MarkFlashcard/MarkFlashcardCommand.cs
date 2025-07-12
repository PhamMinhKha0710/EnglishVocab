using MediatR;

namespace EnglishVocab.Application.Features.StudySessions.Commands.MarkFlashcard
{
    public class MarkFlashcardCommand : IRequest<bool>
    {
        public int SessionId { get; set; }
        public int WordId { get; set; }
        public bool IsKnown { get; set; }
        public int ResponseTimeMs { get; set; }
    }
} 