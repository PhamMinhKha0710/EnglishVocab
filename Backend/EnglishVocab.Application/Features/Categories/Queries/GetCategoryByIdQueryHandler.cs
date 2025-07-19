using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.Categories.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IWordRepository _wordRepository;

        public GetCategoryByIdQueryHandler(
            ICategoryRepository categoryRepository, 
            IMapper mapper,
            IWordRepository wordRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _wordRepository = wordRepository;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
                return null;

            // Map the category to DTO
            var categoryDto = _mapper.Map<CategoryDto>(category);
            
            // Get word count separately
            var words = await _wordRepository.GetByCategoryAsync(request.Id, cancellationToken);
            categoryDto.WordCount = words.Count;
            
            return categoryDto;
        }
    }
} 