using EnglishVocab.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.UserProgress.Commands
{
    public class ResetProgressCommandHandler : IRequestHandler<ResetProgressCommand, bool>
    {
        private readonly IUserProgressRepository _userProgressRepository;
        private readonly IWordSetRepository _wordSetRepository;

        public ResetProgressCommandHandler(
            IUserProgressRepository userProgressRepository,
            IWordSetRepository wordSetRepository)
        {
            _userProgressRepository = userProgressRepository;
            _wordSetRepository = wordSetRepository;
        }

        public async Task<bool> Handle(ResetProgressCommand request, CancellationToken cancellationToken)
        {
            if (request.WordSetId.HasValue)
            {
                // Reset progress for a specific word set
                var wordSet = await _wordSetRepository.GetByIdAsync(request.WordSetId.Value);
                if (wordSet == null)
                {
                    return false;
                }
                
                var wordIds = await _wordSetRepository.GetWordIdsBySetIdAsync(request.WordSetId.Value);
                return await _userProgressRepository.ResetProgressForWordsAsync(request.UserId, wordIds);
            }
            else
            {
                // Reset all progress for the user
                return await _userProgressRepository.ResetAllProgressAsync(request.UserId);
            }
        }
    }
} 