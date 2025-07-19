using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace EnglishVocab.Application.Features.DifficultyLevels.Queries
{
    public class GetDifficultyLevelsQueryHandler : IRequestHandler<GetDifficultyLevelsQuery, object>
    {
        private readonly IDifficultyLevelRepository _difficultyLevelRepository;
        private readonly IDataTableService _dataTableService;
        private readonly IMapper _mapper;

        public GetDifficultyLevelsQueryHandler(
            IDifficultyLevelRepository difficultyLevelRepository,
            IDataTableService dataTableService,
            IMapper mapper)
        {
            _difficultyLevelRepository = difficultyLevelRepository;
            _dataTableService = dataTableService;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDifficultyLevelsQuery request, CancellationToken cancellationToken)
        {
            // Kiểm tra yêu cầu phân trang
            bool usePagination = request.UsePagination && 
                                request.PaginationParams != null && 
                                request.PaginationParams.IsPagingRequest();

            // Đảm bảo tham số phân trang được chuẩn hóa
            if (usePagination)
            {
                request.PaginationParams.NormalizeRequest();
            }

            // Lấy danh sách độ khó từ repository
            var difficultyLevels = await _difficultyLevelRepository.GetAllAsync(cancellationToken);
            
            // Nếu có tìm kiếm, lọc kết quả
            if (!string.IsNullOrWhiteSpace(request.PaginationParams?.Search))
            {
                var searchTerm = request.PaginationParams.Search.ToLower();
                difficultyLevels = difficultyLevels.Where(d => 
                    d.Name.ToLower().Contains(searchTerm) || 
                    (d.Description != null && d.Description.ToLower().Contains(searchTerm))
                ).ToList();
            }
            
            // Ánh xạ sang DTO
            var dtoList = difficultyLevels.Select(d => new DifficultyLevelDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Value = d.Value
            }).ToList();
            
            // Nếu không sử dụng phân trang, trả về toàn bộ danh sách
            if (!usePagination)
            {
                return dtoList;
            }

            // Chuyển đổi từ DataTableRequest sang PaginationParameters
            var paginationParams = new PaginationParameters
            {
                PageNumber = request.PaginationParams.GetPageNumber(),
                PageSize = request.PaginationParams.Length,
                Search = request.PaginationParams.Search,
                OrderBy = request.PaginationParams.OrderBy,
                OrderDirection = request.PaginationParams.Order
            };

            // Sử dụng DataTableService để áp dụng phân trang và sắp xếp
            return _dataTableService.CreatePaginatedResponse(
                dtoList.AsQueryable(),
                paginationParams);
        }
    }
} 