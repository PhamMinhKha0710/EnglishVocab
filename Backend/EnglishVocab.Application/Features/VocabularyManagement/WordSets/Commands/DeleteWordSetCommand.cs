using MediatR;

namespace EnglishVocab.Application.Features.WordSets.Commands
{
    public class DeleteWordSetCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
} 
 