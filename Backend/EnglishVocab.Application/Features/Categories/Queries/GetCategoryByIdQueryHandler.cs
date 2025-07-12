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

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
                return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                WordCount = category.Words?.Count ?? 0
            };
        }
    }
} 