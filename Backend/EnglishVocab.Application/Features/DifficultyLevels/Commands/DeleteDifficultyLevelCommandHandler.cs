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
        private readonly IWordRepository _wordRepository;

        public DeleteDifficultyLevelCommandHandler(
            IDifficultyLevelRepository difficultyLevelRepository,
            IWordRepository wordRepository)
        {
            _difficultyLevelRepository = difficultyLevelRepository;
            _wordRepository = wordRepository;
        }

        public async Task<bool> Handle(DeleteDifficultyLevelCommand request, CancellationToken cancellationToken)
        {
            // Check if the difficulty level exists
            var difficultyLevel = await _difficultyLevelRepository.GetByIdAsync(request.Id, cancellationToken);
            if (difficultyLevel == null)
            {
                throw new NotFoundException("DifficultyLevel", request.Id);
            }
            
            // Check if there are words using this difficulty level
            var wordsWithDifficultyLevel = await _wordRepository.GetByDifficultyLevelAsync(request.Id, cancellationToken);
            if (wordsWithDifficultyLevel.Count > 0)
            {
                throw new System.Exception("Cannot delete a difficulty level that is used by words. Reassign words to another difficulty level first.");
            }

            await _difficultyLevelRepository.DeleteAsync(request.Id, cancellationToken);
            return true;
        }
    }
} 