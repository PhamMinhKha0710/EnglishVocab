using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.StudyManagement.Sessions.Queries.GetNextFlashcard
{
    public class GetNextFlashcardQueryHandler : IRequestHandler<GetNextFlashcardQuery, FlashcardDto>
    {
        private readonly IStudySessionRepository _studySessionRepository;
        private readonly IWordRepository _wordRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDifficultyLevelRepository _difficultyLevelRepository;
        private readonly IMapper _mapper;

        public GetNextFlashcardQueryHandler(
            IStudySessionRepository studySessionRepository,
            IWordRepository wordRepository,
            ICategoryRepository categoryRepository,
            IDifficultyLevelRepository difficultyLevelRepository,
            IMapper mapper)
        {
            _studySessionRepository = studySessionRepository;
            _wordRepository = wordRepository;
            _categoryRepository = categoryRepository;
            _difficultyLevelRepository = difficultyLevelRepository;
            _mapper = mapper;
        }

        public async Task<FlashcardDto> Handle(GetNextFlashcardQuery request, CancellationToken cancellationToken)
        {
            var session = await _studySessionRepository.GetByIdWithDetailsAsync(request.SessionId);
            
            if (session == null || session.Status == "completed")
            {
                return null;
            }
            
            var nextWord = await _studySessionRepository.GetNextWordForSessionAsync(request.SessionId);
            
            if (nextWord == null)
            {
                return null;
            }
            
            var flashcardDto = new FlashcardDto
            {
                WordId = nextWord.Id,
                Text = nextWord.English,
                Translation = nextWord.Vietnamese,
                Pronunciation = nextWord.Pronunciation,
                Examples = !string.IsNullOrEmpty(nextWord.Example) ? new List<string> { nextWord.Example } : new List<string>(),
                Category = string.Empty,
                DifficultyLevel = string.Empty
            };
            
            // Get category name if CategoryId is not null
            if (nextWord.CategoryId.HasValue)
            {
                var category = await _categoryRepository.GetByIdAsync(nextWord.CategoryId.Value, cancellationToken);
                if (category != null)
                {
                    flashcardDto.Category = category.Name;
                }
            }
            
            // Get difficulty level name if DifficultyLevelId is not null
            if (nextWord.DifficultyLevelId.HasValue)
            {
                var difficultyLevel = await _difficultyLevelRepository.GetByIdAsync(nextWord.DifficultyLevelId.Value, cancellationToken);
                if (difficultyLevel != null)
                {
                    flashcardDto.DifficultyLevel = difficultyLevel.Name;
                }
            }
            
            return flashcardDto;
        }
    }
} 