using EnglishVocab.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Words.Commands
{
    public class DeleteWordCommandHandler : IRequestHandler<DeleteWordCommand, bool>
    {
        private readonly IWordRepository _wordRepository;

        public DeleteWordCommandHandler(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public async Task<bool> Handle(DeleteWordCommand request, CancellationToken cancellationToken)
        {
            var word = await _wordRepository.GetByIdAsync(request.Id);
            if (word == null)
            {
                return false;
            }
            
            return await _wordRepository.DeleteAsync(word.Id);
        }
    }
} 