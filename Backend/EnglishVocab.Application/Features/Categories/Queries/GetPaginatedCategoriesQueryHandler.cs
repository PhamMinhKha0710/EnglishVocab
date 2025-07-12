using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.Categories.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace EnglishVocab.Application.Features.Categories.Queries
{
    public class GetPaginatedCategoriesQueryHandler : IRequestHandler<GetPaginatedCategoriesQuery, DataTableResponse<CategoryDto>>
    {
        private readonly IWordRepository _wordRepository;
        private readonly IPaginationService _paginationService;
        private readonly IMapper _mapper;

        public GetPaginatedCategoriesQueryHandler(
            IWordRepository wordRepository,
            IPaginationService paginationService,
            IMapper mapper)
        {
            _wordRepository = wordRepository;
            _paginationService = paginationService;
            _mapper = mapper;
        }

        public async Task<DataTableResponse<CategoryDto>> Handle(GetPaginatedCategoriesQuery request, CancellationToken cancellationToken)
        {
            // Get all categories with word counts
            var categories = await _wordRepository.GetCategoriesWithCountsAsync();
            
            // Create a list of CategoryDto objects
            var categoryDtos = categories.Select(c => new CategoryDto 
            { 
                Name = c.Key, 
                WordCount = c.Value 
            }).ToList();
            
            // Use the pagination service to create a paginated list
            var paginatedCategories = _paginationService.CreatePaginatedList(
                categoryDtos,
                request.PageNumber,
                request.PageSize,
                request.SortBy,
                request.Ascending
            );
            
            return paginatedCategories;
        }
    }
} 