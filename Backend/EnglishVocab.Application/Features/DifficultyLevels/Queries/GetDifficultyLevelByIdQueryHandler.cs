using EnglishVocab.Application.Common.Exceptions;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.DifficultyLevels.Queries
{
    public class GetDifficultyLevelByIdQueryHandler : IRequestHandler<GetDifficultyLevelByIdQuery, DifficultyLevelDto>
    {
        private readonly IDifficultyLevelRepository _difficultyLevelRepository;

        public GetDifficultyLevelByIdQueryHandler(IDifficultyLevelRepository difficultyLevelRepository)
        {
            _difficultyLevelRepository = difficultyLevelRepository;
        }

        public async Task<DifficultyLevelDto> Handle(GetDifficultyLevelByIdQuery request, CancellationToken cancellationToken)
        {
            var difficultyLevel = await _difficultyLevelRepository.GetByIdAsync(request.Id, cancellationToken);

            if (difficultyLevel == null)
            {
                throw new NotFoundException("DifficultyLevel", request.Id);
            }

            return new DifficultyLevelDto
            {
                Id = difficultyLevel.Id,
                Name = difficultyLevel.Name,
                Description = difficultyLevel.Description,
                Value = difficultyLevel.Value
            };
        }
    }
} 