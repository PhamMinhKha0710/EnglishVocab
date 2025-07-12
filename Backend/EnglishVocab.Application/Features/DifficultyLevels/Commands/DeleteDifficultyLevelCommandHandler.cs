using EnglishVocab.Application.Common.Exceptions;
using EnglishVocab.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.DifficultyLevels.Commands
{
    public class DeleteDifficultyLevelCommandHandler : IRequestHandler<DeleteDifficultyLevelCommand, bool>
    {
        private readonly IDifficultyLevelRepository _difficultyLevelRepository;

        public DeleteDifficultyLevelCommandHandler(IDifficultyLevelRepository difficultyLevelRepository)
        {
            _difficultyLevelRepository = difficultyLevelRepository;
        }

        public async Task<bool> Handle(DeleteDifficultyLevelCommand request, CancellationToken cancellationToken)
        {
            var result = await _difficultyLevelRepository.DeleteAsync(request.Id, cancellationToken);

            if (!result)
            {
                throw new NotFoundException("DifficultyLevel", request.Id);
            }

            return true;
        }
    }
} 