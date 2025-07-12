using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.WordSets.Queries.GetAllWordSets;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.WordSets.Queries.GetDefaultWordSets
{
    public class GetDefaultWordSetsQueryHandler : IRequestHandler<GetDefaultWordSetsQuery, IEnumerable<WordSetDto>>
    {
        private readonly IWordSetRepository _wordSetRepository;
        private readonly IMapper _mapper;

        public GetDefaultWordSetsQueryHandler(IWordSetRepository wordSetRepository, IMapper mapper)
        {
            _wordSetRepository = wordSetRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WordSetDto>> Handle(GetDefaultWordSetsQuery request, CancellationToken cancellationToken)
        {
            var wordSets = await _wordSetRepository.GetDefaultSetsAsync();
            return _mapper.Map<IEnumerable<WordSetDto>>(wordSets);
        }
    }
} 