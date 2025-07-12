using EnglishVocab.Application.Features.WordSets.Queries.GetAllWordSets;
using MediatR;

namespace EnglishVocab.Application.Features.WordSets.Commands
{
    public class CreateWordSetCommand : IRequest<WordSetDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public int CreatedById { get; set; }
    }
} 
 