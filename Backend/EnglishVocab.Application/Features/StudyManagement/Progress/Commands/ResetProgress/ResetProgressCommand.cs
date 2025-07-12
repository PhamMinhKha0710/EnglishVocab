using MediatR;

namespace EnglishVocab.Application.Features.UserProgress.Commands
{
    public class ResetProgressCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public int? WordSetId { get; set; } // Optional - if provided, only reset progress for this word set
    }
} 