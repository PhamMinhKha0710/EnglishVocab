using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.WordSets.Queries.GetAllWordSets;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.WordSets.Queries.GetUserWordSets
{
    public class GetUserWordSetsQueryHandler : IRequestHandler<GetUserWordSetsQuery, IEnumerable<WordSetDto>>
    {
        private readonly IWordSetRepository _wordSetRepository;
        private readonly IMapper _mapper;

        public GetUserWordSetsQueryHandler(IWordSetRepository wordSetRepository, IMapper mapper)
        {
            _wordSetRepository = wordSetRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WordSetDto>> Handle(GetUserWordSetsQuery request, CancellationToken cancellationToken)
        {
            var wordSets = await _wordSetRepository.GetByUserIdAsync(request.UserId);
            return _mapper.Map<IEnumerable<WordSetDto>>(wordSets);
        }
    }
} 