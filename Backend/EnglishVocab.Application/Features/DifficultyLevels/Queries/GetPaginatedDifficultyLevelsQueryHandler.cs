using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.DifficultyLevels.Queries
{
    public class GetPaginatedDifficultyLevelsQueryHandler : IRequestHandler<GetPaginatedDifficultyLevelsQuery, DataTableResponse<DifficultyLevelDto>>
    {
        private readonly IDifficultyLevelRepository _difficultyLevelRepository;

        public GetPaginatedDifficultyLevelsQueryHandler(IDifficultyLevelRepository difficultyLevelRepository)
        {
            _difficultyLevelRepository = difficultyLevelRepository;
        }

        public async Task<DataTableResponse<DifficultyLevelDto>> Handle(GetPaginatedDifficultyLevelsQuery request, CancellationToken cancellationToken)
        {
            return await _difficultyLevelRepository.GetPaginatedDifficultyLevelsAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }
    }
} 