using EnglishVocab.Application.Common.Exceptions;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetWordByIdQueryHandler : IRequestHandler<GetWordByIdQuery, WordDto>
    {
        private readonly IApplicationDbContext _context;

        public GetWordByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WordDto> Handle(GetWordByIdQuery request, CancellationToken cancellationToken)
        {
            var word = await _context.Words
                .Include(w => w.CategoryEntity)
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (word == null)
            {
                throw new NotFoundException("Word", request.Id);
            }

            return new WordDto
            {
                Id = word.Id,
                English = word.English,
                Vietnamese = word.Vietnamese,
                Pronunciation = word.Pronunciation,
                Example = word.Example,
                Notes = word.Notes,
                ImageUrl = word.ImageUrl,
                AudioUrl = word.AudioUrl,
                DifficultyLevel = word.DifficultyLevel.ToString(),
                CategoryId = word.CategoryId,
                CategoryName = word.CategoryEntity != null ? word.CategoryEntity.Name : word.Category
            };
        }
    }
} 