using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.VocabularyManagement.WordSets.DTOs;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.WordSets.Queries.GetWordSetById
{
    public class GetWordSetByIdQueryHandler : IRequestHandler<GetWordSetByIdQuery, WordSetDetailDto>
    {
        private readonly IWordSetRepository _wordSetRepository;
        private readonly IMapper _mapper;

        public GetWordSetByIdQueryHandler(IWordSetRepository wordSetRepository, IMapper mapper)
        {
            _wordSetRepository = wordSetRepository;
            _mapper = mapper;
        }

        public async Task<WordSetDetailDto> Handle(GetWordSetByIdQuery request, CancellationToken cancellationToken)
        {
            var wordSet = await _wordSetRepository.GetByIdAsync(request.Id);
            if (wordSet == null)
            {
                return null;
            }
            
            var words = await _wordSetRepository.GetWordsBySetIdAsync(request.Id);
            var wordSetDetail = _mapper.Map<WordSetDetailDto>(wordSet);
            wordSetDetail.Words = _mapper.Map<System.Collections.Generic.List<WordDto>>(words);
            
            return wordSetDetail;
        }
    }
} 