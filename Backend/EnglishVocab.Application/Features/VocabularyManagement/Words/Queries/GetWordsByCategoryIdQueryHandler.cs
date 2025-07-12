using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetWordsByCategoryIdQueryHandler : IRequestHandler<GetWordsByCategoryIdQuery, IEnumerable<WordDto>>
    {
        private readonly IWordService _wordService;
        private readonly IMapper _mapper;

        public GetWordsByCategoryIdQueryHandler(IWordService wordService, IMapper mapper)
        {
            _wordService = wordService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WordDto>> Handle(GetWordsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var words = await _wordService.GetWordsByCategoryIdAsync(request.CategoryId);
            return _mapper.Map<IEnumerable<WordDto>>(words);
        }
    }
} 