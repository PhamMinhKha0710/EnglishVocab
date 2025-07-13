using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.DifficultyLevels.Queries
{
    public class GetDifficultyLevelsQueryHandler : IRequestHandler<GetDifficultyLevelsQuery, object>
    {
        private readonly IDifficultyLevelRepository _difficultyLevelRepository;
        private readonly IDataTableService _dataTableService;

        public GetDifficultyLevelsQueryHandler(
            IDifficultyLevelRepository difficultyLevelRepository,
            IDataTableService dataTableService)
        {
            _difficultyLevelRepository = difficultyLevelRepository;
            _dataTableService = dataTableService;
        }

        public async Task<object> Handle(GetDifficultyLevelsQuery request, CancellationToken cancellationToken)
        {
            // Lấy tất cả danh sách difficulty levels
            var difficultyLevels = await _difficultyLevelRepository.GetAllDifficultyLevelsAsync(cancellationToken);
            
            // Nếu không sử dụng phân trang, trả về toàn bộ danh sách
            if (!request.UsePagination)
            {
                return difficultyLevels;
            }

            // Nếu sử dụng phân trang, tạo DataTableResponse
            return await _dataTableService.CreateResponseAsync(
                difficultyLevels,
                request.PaginationParams,
                cancellationToken);
        }
    }
} 