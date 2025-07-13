using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.Categories.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Categories.Queries
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, object>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWordRepository _wordRepository;
        private readonly IDataTableService _dataTableService;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(
            ICategoryRepository categoryRepository, 
            IWordRepository wordRepository,
            IDataTableService dataTableService,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _wordRepository = wordRepository;
            _dataTableService = dataTableService;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            // Lấy tất cả categories và đếm số lượng từ trong mỗi category
            var categoriesWithCounts = await _wordRepository.GetCategoriesWithCountsAsync();
            
            // Tạo danh sách CategoryDto
            var categoryDtos = categoriesWithCounts.Select(c => new CategoryDto 
            { 
                Name = c.Key, 
                WordCount = c.Value 
            }).ToList();

            // Nếu không sử dụng phân trang, trả về toàn bộ danh sách
            if (!request.UsePagination)
            {
                return categoryDtos;
            }

            // Nếu sử dụng phân trang, tạo DataTableResponse
            return await _dataTableService.CreateResponseAsync(
                categoryDtos, 
                request.PaginationParams,
                cancellationToken);
        }
    }
} 