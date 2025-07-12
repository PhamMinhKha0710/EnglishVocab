using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.Categories.DTOs;
using EnglishVocab.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Check if category already exists
            var existingCategory = await _categoryRepository.GetByNameAsync(request.Name);
            if (existingCategory != null)
            {
                throw new Exception($"Category with name '{request.Name}' already exists.");
            }

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                DateCreated = DateTime.UtcNow
            };

            var createdCategory = await _categoryRepository.AddAsync(category);
            
            return new CategoryDto
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name,
                Description = createdCategory.Description,
                WordCount = 0 // New category has no words yet
            };
        }
    }
} 