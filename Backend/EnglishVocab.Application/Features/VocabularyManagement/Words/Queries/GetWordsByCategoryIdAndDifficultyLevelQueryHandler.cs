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
            // Converter a string para int (assumindo que o DifficultyLevel é um enum com valores inteiros)
            int difficultyLevelId;
            if (!int.TryParse(request.DifficultyLevel, out difficultyLevelId))
            {
                // Se não for possível converter, tente converter de enum para int
                if (Enum.TryParse<DifficultyLevelType>(request.DifficultyLevel, out var difficultyLevelEnum))
                {
                    difficultyLevelId = (int)difficultyLevelEnum;
                }
                else
                {
                    // Se não for possível converter, retorne uma lista vazia
                    return new List<WordDto>();
                }
            }

            var words = await _wordRepository.GetByCategoryIdAndDifficultyLevelAsync(request.CategoryId, difficultyLevelId, cancellationToken);

            return _mapper.Map<IEnumerable<WordDto>>(words);
        }
    }
} 