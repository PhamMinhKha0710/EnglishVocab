using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetRecentlyStudied;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetRecentlyStudied
{
    public class GetRecentlyStudiedQueryHandler : IRequestHandler<GetRecentlyStudiedQuery, IEnumerable<WordProgressDto>>
    {
        private readonly IUserProgressRepository _userProgressRepository;
        private readonly IWordRepository _wordRepository;
        private readonly IMapper _mapper;

        public GetRecentlyStudiedQueryHandler(
            IUserProgressRepository userProgressRepository,
            IWordRepository wordRepository,
            IMapper mapper)
        {
            _userProgressRepository = userProgressRepository;
            _wordRepository = wordRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WordProgressDto>> Handle(GetRecentlyStudiedQuery request, CancellationToken cancellationToken)
        {
            var recentProgress = await _userProgressRepository.GetRecentlyStudiedAsync(
                request.UserId, 
                request.Count);
                
            if (recentProgress == null || !recentProgress.Any())
            {
                return new List<WordProgressDto>();
            }
            
            // Map to WordProgressDto directly using the UserProgressWithWordInfo model
            var result = new List<WordProgressDto>();
            foreach (var progress in recentProgress)
            {
                result.Add(new WordProgressDto
                {
                    Id = progress.Progress.Id,
                    WordId = progress.Progress.WordId,
                    Word = progress.WordText,
                    Translation = progress.WordTranslation,
                    Category = progress.Category,
                    DifficultyLevel = progress.DifficultyLevel,
                    MasteryLevel = progress.Progress.MasteryLevel.ToString(),
                    LastReviewed = progress.Progress.LastReviewed,
                    NextReviewDue = progress.Progress.NextReviewDate,
                    ReviewCount = progress.Progress.TimesReviewed,
                    SuccessRate = progress.Progress.SuccessRate
                });
            }
            
            return result;
        }
    }
} 