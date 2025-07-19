using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetDueForReview;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetDueForReview
{
    public class GetDueForReviewQueryHandler : IRequestHandler<GetDueForReviewQuery, IEnumerable<WordProgressDto>>
    {
        private readonly IUserProgressRepository _userProgressRepository;
        private readonly IWordRepository _wordRepository;
        private readonly IMapper _mapper;

        public GetDueForReviewQueryHandler(
            IUserProgressRepository userProgressRepository,
            IWordRepository wordRepository,
            IMapper mapper)
        {
            _userProgressRepository = userProgressRepository;
            _wordRepository = wordRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WordProgressDto>> Handle(GetDueForReviewQuery request, CancellationToken cancellationToken)
        {
            var dueDate = DateTime.UtcNow;
            var dueProgress = await _userProgressRepository.GetDueForReviewAsync(request.UserId, request.Count);
            
            if (dueProgress == null || !dueProgress.Any())
            {
                return new List<WordProgressDto>();
            }
            
            // Extrair os IDs das palavras e converter para int
            var wordIds = dueProgress.Select(p => p.WordId).ToList();
            
            // Buscar todas as palavras com detalhes relacionados
            var allWordsWithDetails = await _wordRepository.GetAllWithDetailsAsync(cancellationToken: cancellationToken);
            
            // Filtrar apenas as palavras que estão na lista de IDs
            var wordsWithDetails = allWordsWithDetails.Where(w => wordIds.Contains(w.Word.Id)).ToList();
            
            // Map to WordProgressDto
            var result = new List<WordProgressDto>();
            foreach (var progress in dueProgress)
            {
                var wordDetail = wordsWithDetails.FirstOrDefault(w => w.Word.Id == progress.WordId);
                if (wordDetail.Word != null)
                {
                    var word = wordDetail.Word;
                    var category = wordDetail.Category;
                    var difficultyLevel = wordDetail.DifficultyLevel;
                    
                    result.Add(new WordProgressDto
                    {
                        Id = progress.Id,
                        WordId = word.Id,
                        Word = word.English,
                        Translation = word.Vietnamese,
                        Category = category?.Name ?? "Unknown",
                        DifficultyLevel = difficultyLevel?.Name ?? "Unknown",
                        MasteryLevel = progress.MasteryLevel.ToString(),
                        LastReviewed = progress.LastReviewed,
                        NextReviewDue = progress.NextReviewDate,
                        ReviewCount = progress.TimesReviewed,
                        SuccessRate = progress.SuccessRate
                    });
                }
            }
            
            return result;
        }
    }
} 