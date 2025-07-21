using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.Categories.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Categories.Queries
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, object>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDataTableService _dataTableService;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository, IDataTableService dataTableService, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _dataTableService = dataTableService;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
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

            // Lấy danh sách danh mục từ repository
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            
            // Nếu có tìm kiếm, lọc kết quả
            if (!string.IsNullOrWhiteSpace(request.PaginationParams?.Search))
            {
                var searchTerm = request.PaginationParams.Search.ToLower();
                categories = categories.Where(c => 
                    c.Name.ToLower().Contains(searchTerm) || 
                    (c.Description != null && c.Description.ToLower().Contains(searchTerm))
                ).ToList();
            }
            
            // Ánh xạ sang DTO
            var dtoList = _mapper.Map<List<CategoryDto>>(categories);

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