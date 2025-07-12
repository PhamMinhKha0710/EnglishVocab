using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.DifficultyLevels.Queries
{
    public class GetDataTableDifficultyLevelsQuery : DataTableRequest, IRequest<DataTableResponse<DifficultyLevelDto>>
    {
        // Không cần thêm thuộc tính vì đã kế thừa từ DataTableRequest
    }
} 