using AutoMapper;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EnglishVocab.Constants.Constant;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Commands
{
    public class CreateWordCommandHandler : IRequestHandler<CreateWordCommand, WordDto>
    {
        private readonly IWordRepository _wordRepository;
        private readonly IMapper _mapper;

        public CreateWordCommandHandler(IWordRepository wordRepository, IMapper mapper)
        {
            _wordRepository = wordRepository;
            _mapper = mapper;
        }

        public async Task<WordDto> Handle(CreateWordCommand request, CancellationToken cancellationToken)
        {
            var entity = new Word
            {
                English = request.English,
                Vietnamese = request.Vietnamese,
                Pronunciation = request.Pronunciation,
                Example = request.Example,
                Notes = request.Notes,
                ImageUrl = request.ImageUrl,
                AudioUrl = request.AudioUrl,
                DifficultyLevel = Enum.Parse<DifficultyLevelType>(request.DifficultyLevel),
                CategoryId = request.CategoryId
            };

            var createdEntity = await _wordRepository.AddAsync(entity);
            await _wordRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WordDto>(createdEntity);
        }
    }
} 
 