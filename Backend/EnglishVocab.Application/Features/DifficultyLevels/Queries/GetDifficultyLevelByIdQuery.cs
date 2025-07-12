using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.DifficultyLevels.Queries
{
    public class GetDifficultyLevelByIdQuery : IRequest<DifficultyLevelDto>
    {
        public int Id { get; set; }
    }
} 