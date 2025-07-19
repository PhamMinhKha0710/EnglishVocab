using AutoMapper;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Commands
{
    public class CreateWordCommandHandler : IRequestHandler<CreateWordCommand, WordDto>
    {
        private readonly IWordRepository _wordRepository;
        private readonly IDifficultyLevelRepository _difficultyLevelRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateWordCommandHandler(
            IWordRepository wordRepository, 
            IDifficultyLevelRepository difficultyLevelRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _wordRepository = wordRepository;
            _difficultyLevelRepository = difficultyLevelRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<WordDto> Handle(CreateWordCommand request, CancellationToken cancellationToken)
        {
            // Get difficulty level by name
            var difficultyLevel = await _difficultyLevelRepository.GetByNameAsync(request.DifficultyLevel, cancellationToken);
            if (difficultyLevel == null)
            {
                throw new Exception($"Difficulty level '{request.DifficultyLevel}' not found");
            }
            
            var entity = new Word
            {
                English = request.English,
                Vietnamese = request.Vietnamese,
                Pronunciation = request.Pronunciation,
                Example = request.Example,
                Notes = request.Notes,
                ImageUrl = request.ImageUrl,
                AudioUrl = request.AudioUrl,
                DifficultyLevelId = difficultyLevel.Id,
                CategoryId = request.CategoryId
            };

            var createdEntity = await _wordRepository.AddAsync(entity, cancellationToken);
            
            // Map to DTO
            var wordDto = _mapper.Map<WordDto>(createdEntity);
            
            // Add related information
            if (entity.CategoryId.HasValue)
            {
                var category = await _categoryRepository.GetByIdAsync(entity.CategoryId.Value, cancellationToken);
                if (category != null)
                {
                    wordDto.CategoryName = category.Name;
                }
            }
            
            wordDto.DifficultyLevelName = difficultyLevel.Name;
            wordDto.DifficultyValue = difficultyLevel.Value;
            
            return wordDto;
        }
    }
} 
 