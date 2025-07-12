using EnglishVocab.Application.Common.Exceptions;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.DifficultyLevels.Commands
{
    public class UpdateDifficultyLevelCommandHandler : IRequestHandler<UpdateDifficultyLevelCommand, DifficultyLevelDto>
    {
        private readonly IDifficultyLevelRepository _difficultyLevelRepository;

        public UpdateDifficultyLevelCommandHandler(IDifficultyLevelRepository difficultyLevelRepository)
        {
            _difficultyLevelRepository = difficultyLevelRepository;
        }

        public async Task<DifficultyLevelDto> Handle(UpdateDifficultyLevelCommand request, CancellationToken cancellationToken)
        {
            var difficultyLevel = await _difficultyLevelRepository.GetByIdAsync(request.Id, cancellationToken);

            if (difficultyLevel == null)
            {
                throw new NotFoundException("DifficultyLevel", request.Id);
            }

            // Cập nhật thuộc tính
            difficultyLevel.Name = request.Name;
            difficultyLevel.Description = request.Description;
            difficultyLevel.Value = request.Value;

            // Lưu thay đổi
            await _difficultyLevelRepository.UpdateAsync(difficultyLevel, cancellationToken);

            // Trả về DTO
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