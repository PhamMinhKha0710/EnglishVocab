using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using EnglishVocab.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.DifficultyLevels.Commands
{
    public class CreateDifficultyLevelCommandHandler : IRequestHandler<CreateDifficultyLevelCommand, DifficultyLevelDto>
    {
        private readonly IDifficultyLevelRepository _difficultyLevelRepository;

        public CreateDifficultyLevelCommandHandler(IDifficultyLevelRepository difficultyLevelRepository)
        {
            _difficultyLevelRepository = difficultyLevelRepository;
        }

        public async Task<DifficultyLevelDto> Handle(CreateDifficultyLevelCommand request, CancellationToken cancellationToken)
        {
            var difficultyLevel = new DifficultyLevel
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value
            };

            var result = await _difficultyLevelRepository.AddAsync(difficultyLevel, cancellationToken);

            return new DifficultyLevelDto
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Value = result.Value
            };
        }
    }
} 