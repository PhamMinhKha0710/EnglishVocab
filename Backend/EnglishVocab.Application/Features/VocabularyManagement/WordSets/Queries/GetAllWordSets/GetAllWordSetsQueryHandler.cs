using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.VocabularyManagement.WordSets.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.VocabularyManagement.WordSets.Queries.GetAllWordSets
{
    public class GetAllWordSetsQueryHandler : IRequestHandler<GetAllWordSetsQuery, IEnumerable<WordSetDto>>
    {
        private readonly IWordSetRepository _wordSetRepository;
        private readonly IMapper _mapper;

        public GetAllWordSetsQueryHandler(IWordSetRepository wordSetRepository, IMapper mapper)
        {
            _wordSetRepository = wordSetRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WordSetDto>> Handle(GetAllWordSetsQuery request, CancellationToken cancellationToken)
        {
            var wordSets = await _wordSetRepository.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                request.SortBy,
                request.Ascending);
                
            return _mapper.Map<IEnumerable<WordSetDto>>(wordSets);
        }
    }
} 