using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class SearchWordsQueryHandler : IRequestHandler<SearchWordsQuery, IEnumerable<WordDto>>
    {
        private readonly IWordRepository _wordRepository;
        private readonly IMapper _mapper;

        public SearchWordsQueryHandler(IWordRepository wordRepository, IMapper mapper)
        {
            _wordRepository = wordRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WordDto>> Handle(SearchWordsQuery request, CancellationToken cancellationToken)
        {
            var words = await _wordRepository.SearchAsync(
                request.SearchTerm,
                request.Category,
                request.DifficultyLevel,
                request.PageNumber,
                request.PageSize);
                
            return _mapper.Map<IEnumerable<WordDto>>(words);
        }
    }
} 