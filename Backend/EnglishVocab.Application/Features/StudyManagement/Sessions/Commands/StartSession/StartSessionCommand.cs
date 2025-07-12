using EnglishVocab.Application.Features.StudySessions.Queries;
using EnglishVocab.Constants.Constant;
using MediatR;

namespace EnglishVocab.Application.Features.StudySessions.Commands.StartSession
{
    public class StartSessionCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public int WordSetId { get; set; }
        public StudyMode StudyMode { get; set; } = StudyMode.Flashcard;
        public bool ShuffleWords { get; set; } = true;
    }
} 