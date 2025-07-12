using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.DifficultyLevels.Queries
{
    public class GetDifficultyLevelsQuery : IRequest<IEnumerable<DifficultyLevelDto>>
    {
    }
} 