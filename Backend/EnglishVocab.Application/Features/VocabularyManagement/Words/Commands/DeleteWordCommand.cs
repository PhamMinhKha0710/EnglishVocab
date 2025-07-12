using MediatR;

namespace EnglishVocab.Application.Features.Words.Commands
{
    public class DeleteWordCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
} 