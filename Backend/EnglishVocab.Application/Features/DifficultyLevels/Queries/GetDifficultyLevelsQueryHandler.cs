using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.DifficultyLevels.Queries
{
    public class GetDifficultyLevelsQueryHandler : IRequestHandler<GetDifficultyLevelsQuery, IEnumerable<DifficultyLevelDto>>
    {
        private readonly IDifficultyLevelRepository _difficultyLevelRepository;

        public GetDifficultyLevelsQueryHandler(IDifficultyLevelRepository difficultyLevelRepository)
        {
            _difficultyLevelRepository = difficultyLevelRepository;
        }

        public async Task<IEnumerable<DifficultyLevelDto>> Handle(GetDifficultyLevelsQuery request, CancellationToken cancellationToken)
        {
            return await _difficultyLevelRepository.GetAllDifficultyLevelsAsync(cancellationToken);
        }
    }
} 