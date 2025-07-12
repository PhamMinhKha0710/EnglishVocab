using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnglishVocab.Constants.Constant;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetWordsByCategoryIdAndDifficultyLevelQueryHandler : IRequestHandler<GetWordsByCategoryIdAndDifficultyLevelQuery, IEnumerable<WordDto>>
    {
        private readonly IWordRepository _wordRepository;
        private readonly IMapper _mapper;

        public GetWordsByCategoryIdAndDifficultyLevelQueryHandler(IWordRepository wordRepository, IMapper mapper)
        {
            _wordRepository = wordRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WordDto>> Handle(GetWordsByCategoryIdAndDifficultyLevelQuery request, CancellationToken cancellationToken)
        {
            var difficultyLevel = Enum.Parse<DifficultyLevelType>(request.DifficultyLevel);

            var words = await _wordRepository.GetByCategoryIdAndDifficultyLevelAsync(request.CategoryId, request.DifficultyLevel);

            return _mapper.Map<IEnumerable<WordDto>>(words);
        }
    }
} 