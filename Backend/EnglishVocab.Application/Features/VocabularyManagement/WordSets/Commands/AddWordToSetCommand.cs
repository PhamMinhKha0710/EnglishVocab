using MediatR;

namespace EnglishVocab.Application.Features.WordSets.Commands
{
    public class AddWordToSetCommand : IRequest<bool>
    {
        public int WordSetId { get; set; }
        public int WordId { get; set; }
    }
} 
 