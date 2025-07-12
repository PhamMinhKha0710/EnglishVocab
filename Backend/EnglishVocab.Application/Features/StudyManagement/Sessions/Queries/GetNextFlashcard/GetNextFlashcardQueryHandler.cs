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
        private readonly IMapper _mapper;

        public GetNextFlashcardQueryHandler(
            IStudySessionRepository studySessionRepository,
            IWordRepository wordRepository,
            IMapper mapper)
        {
            _studySessionRepository = studySessionRepository;
            _wordRepository = wordRepository;
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
            
            return new FlashcardDto
            {
                WordId = nextWord.Id,
                Text = nextWord.English,
                Translation = nextWord.Vietnamese,
                Pronunciation = nextWord.Pronunciation,
                Examples = !string.IsNullOrEmpty(nextWord.Example) ? new List<string> { nextWord.Example } : new List<string>(),
                Category = nextWord.Category,
                DifficultyLevel = nextWord.DifficultyLevel.ToString()
            };
        }
    }
} 