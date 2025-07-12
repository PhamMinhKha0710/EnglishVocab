using EnglishVocab.Application.Features.WordSets.Queries.GetAllWordSets;
using MediatR;

namespace EnglishVocab.Application.Features.WordSets.Commands
{
    public class UpdateWordSetCommand : IRequest<WordSetDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
    }
} 
 