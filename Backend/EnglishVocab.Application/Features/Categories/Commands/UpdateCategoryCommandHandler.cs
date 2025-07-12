using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.Categories.DTOs;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return null;
            }

            // Check if the new name conflicts with an existing category
            if (category.Name != request.Name)
            {
                var existingWithSameName = await _categoryRepository.GetByNameAsync(request.Name);
                if (existingWithSameName != null && existingWithSameName.Id != request.Id)
                {
                    throw new Exception($"Category with name '{request.Name}' already exists.");
                }
            }

            // Update category
            category.Name = request.Name;
            category.Description = request.Description;
            category.DateModified = DateTime.UtcNow;

            var updatedCategory = await _categoryRepository.UpdateAsync(category);
            
            return new CategoryDto
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                Description = updatedCategory.Description,
                WordCount = updatedCategory.Words?.Count ?? 0
            };
        }
    }
} 