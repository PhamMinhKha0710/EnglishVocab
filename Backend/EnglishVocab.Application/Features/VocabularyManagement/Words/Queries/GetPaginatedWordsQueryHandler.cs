using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetPaginatedWordsQueryHandler : IRequestHandler<GetPaginatedWordsQuery, DataTableResponse<WordDto>>
    {
        private readonly IWordRepository _wordRepository;
        private readonly IMapper _mapper;
        private readonly IDataTableService _dataTableService;

        public GetPaginatedWordsQueryHandler(
            IWordRepository wordRepository, 
            IMapper mapper,
            IDataTableService dataTableService)
        {
            _wordRepository = wordRepository;
            _mapper = mapper;
            _dataTableService = dataTableService;
        }

        public async Task<DataTableResponse<WordDto>> Handle(GetPaginatedWordsQuery request, CancellationToken cancellationToken)
        {
            // Tạo query cơ bản
            var words = await _wordRepository.GetAllAsync();
            var query = words.AsQueryable();
            
            // Áp dụng các bộ lọc
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(w => 
                    w.English.Contains(request.SearchTerm) || 
                    w.Vietnamese.Contains(request.SearchTerm) ||
                    (w.Example != null && w.Example.Contains(request.SearchTerm)));
            }

            if (!string.IsNullOrEmpty(request.Category))
            {
                query = query.Where(w => w.CategoryEntity != null && w.CategoryEntity.Name == request.Category);
            }

            if (!string.IsNullOrEmpty(request.DifficultyLevel))
            {
                query = query.Where(w => w.DifficultyLevel.ToString() == request.DifficultyLevel);
            }
            
            // Chuyển đổi sang DTO trước khi phân trang
            var dtoQuery = query.Select(w => _mapper.Map<WordDto>(w));

            // Chuyển đổi từ PaginatedQuery sang DataTableRequest
            var dataTableRequest = new DataTableRequest
            {
                Start = (request.PageNumber - 1) * request.PageSize,
                Length = request.PageSize,
                OrderBy = request.SortBy,
                Order = request.Ascending ? "asc" : "desc"
            };
            
            // Sử dụng DataTableService thay cho PaginationService
            return await _dataTableService.CreateResponseAsync(
                dtoQuery,
                dataTableRequest,
                cancellationToken
            );
        }
    }
} 
 