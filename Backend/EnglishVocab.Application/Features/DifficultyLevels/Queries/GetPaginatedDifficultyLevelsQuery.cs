using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.DifficultyLevels.Queries
{
    public class GetPaginatedDifficultyLevelsQuery : IRequest<DataTableResponse<DifficultyLevelDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Id";
        public bool Ascending { get; set; } = true;
    }
} 