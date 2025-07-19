using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using EnglishVocab.Application.Features.VocabularyManagement.Words.Queries;
using MediatR;
using System.Linq;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetAllWordsQueryHandler : IRequestHandler<GetAllWordsQuery, IEnumerable<WordDto>>
    {
        private readonly IWordRepository _wordRepository;
        private readonly IMapper _mapper;

        public GetAllWordsQueryHandler(
            IWordRepository wordRepository,
            IMapper mapper)
        {
            _wordRepository = wordRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WordDto>> Handle(GetAllWordsQuery request, CancellationToken cancellationToken)
        {
            var wordsWithDetails = await _wordRepository.GetAllWithDetailsAsync(
                request.SearchTerm, 
                request.CategoryId, 
                request.DifficultyLevelId, 
                cancellationToken);
            
            var wordDtos = new List<WordDto>();
            
            foreach (var (word, category, difficultyLevel) in wordsWithDetails)
            {
                var wordDto = _mapper.Map<WordDto>(word);
                
                if (category != null)
                {
                    wordDto.CategoryName = category.Name;
                }
                
                if (difficultyLevel != null)
                {
                    wordDto.DifficultyLevelName = difficultyLevel.Name;
                    wordDto.DifficultyValue = difficultyLevel.Value;
                }
                
                wordDtos.Add(wordDto);
            }
            
            return wordDtos;
        }
    }
} 