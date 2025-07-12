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
        private readonly IPaginationService _paginationService;

        public GetPaginatedWordsQueryHandler(
            IWordRepository wordRepository, 
            IMapper mapper,
            IPaginationService paginationService)
        {
            _wordRepository = wordRepository;
            _mapper = mapper;
            _paginationService = paginationService;
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

            // Sử dụng IPaginationService để phân trang
            return await _paginationService.CreatePaginatedListAsync(
                dtoQuery,
                request.PageNumber,
                request.PageSize,
                request.SortBy,
                request.Ascending);
        }
    }
} 
 