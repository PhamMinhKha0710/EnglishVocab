using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetProgressByWord;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetProgressByWord
{
    public class GetProgressByWordQueryHandler : IRequestHandler<GetProgressByWordQuery, WordProgressDto>
    {
        private readonly IUserProgressRepository _userProgressRepository;
        private readonly IWordRepository _wordRepository;
        private readonly IMapper _mapper;

        public GetProgressByWordQueryHandler(
            IUserProgressRepository userProgressRepository,
            IWordRepository wordRepository,
            IMapper mapper)
        {
            _userProgressRepository = userProgressRepository;
            _wordRepository = wordRepository;
            _mapper = mapper;
        }

        public async Task<WordProgressDto> Handle(GetProgressByWordQuery request, CancellationToken cancellationToken)
        {
            var word = await _wordRepository.GetByIdAsync(request.WordId);
            if (word == null)
                return null;

            var progress = await _userProgressRepository.GetByUserIdAndWordIdAsync(request.UserId, request.WordId);
            if (progress == null)
                return new WordProgressDto
                {
                    WordId = word.Id,
                    Word = word.English,
                    Translation = word.Vietnamese,
                    Category = word.Category,
                    DifficultyLevel = word.DifficultyLevel.ToString(),
                    MasteryLevel = "0",
                    LastReviewed = DateTime.MinValue,
                    NextReviewDue = DateTime.MinValue,
                    ReviewCount = 0,
                    SuccessRate = 0
                };

            return new WordProgressDto
            {
                Id = progress.Id,
                WordId = word.Id,
                Word = word.English,
                Translation = word.Vietnamese,
                Category = word.Category,
                DifficultyLevel = word.DifficultyLevel.ToString(),
                MasteryLevel = progress.MasteryLevel.ToString(),
                LastReviewed = progress.LastReviewed,
                NextReviewDue = progress.NextReviewDate,
                ReviewCount = progress.TimesReviewed,
                SuccessRate = progress.SuccessRate
            };
        }
    }
} 