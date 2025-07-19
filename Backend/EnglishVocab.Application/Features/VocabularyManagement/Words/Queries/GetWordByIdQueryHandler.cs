using EnglishVocab.Application.Common.Exceptions;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetWordByIdQueryHandler : IRequestHandler<GetWordByIdQuery, WordDto>
    {
        private readonly IWordRepository _wordRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDifficultyLevelRepository _difficultyLevelRepository;

        public GetWordByIdQueryHandler(
            IWordRepository wordRepository, 
            ICategoryRepository categoryRepository,
            IDifficultyLevelRepository difficultyLevelRepository)
        {
            _wordRepository = wordRepository;
            _categoryRepository = categoryRepository;
            _difficultyLevelRepository = difficultyLevelRepository;
        }

        public async Task<WordDto> Handle(GetWordByIdQuery request, CancellationToken cancellationToken)
        {
            var word = await _wordRepository.GetByIdAsync(request.Id, cancellationToken);

            if (word == null)
            {
                throw new NotFoundException("Word", request.Id);
            }

            // Create the DTO
            var wordDto = new WordDto
            {
                Id = word.Id,
                English = word.English,
                Vietnamese = word.Vietnamese,
                Pronunciation = word.Pronunciation,
                Example = word.Example,
                Notes = word.Notes,
                ImageUrl = word.ImageUrl,
                AudioUrl = word.AudioUrl,
                CategoryId = word.CategoryId,
                DifficultyLevelId = word.DifficultyLevelId,
                DateCreated = word.DateCreated,
                DateModified = word.DateModified,
                CreatedBy = word.CreatedBy,
                ModifiedBy = word.ModifiedBy
            };

            // If there's a category ID, get the category name
            if (word.CategoryId.HasValue)
            {
                var category = await _categoryRepository.GetByIdAsync(word.CategoryId.Value, cancellationToken);
                if (category != null)
                {
                    wordDto.CategoryName = category.Name;
                }
            }
            
            // If there's a difficulty level ID, get the difficulty level name and value
            if (word.DifficultyLevelId.HasValue)
            {
                var difficultyLevel = await _difficultyLevelRepository.GetByIdAsync(word.DifficultyLevelId.Value, cancellationToken);
                if (difficultyLevel != null)
                {
                    wordDto.DifficultyLevelName = difficultyLevel.Name;
                    wordDto.DifficultyValue = difficultyLevel.Value;
                }
            }

            return wordDto;
        }
    }
} 