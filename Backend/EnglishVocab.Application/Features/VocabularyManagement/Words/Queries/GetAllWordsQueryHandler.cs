using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetAllWordsQueryHandler : IRequestHandler<GetAllWordsQuery, IEnumerable<WordDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllWordsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WordDto>> Handle(GetAllWordsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Words
                .Select(w => new WordDto
                {
                    Id = w.Id,
                    English = w.English,
                    Vietnamese = w.Vietnamese,
                    Pronunciation = w.Pronunciation,
                    Example = w.Example,
                    Notes = w.Notes,
                    ImageUrl = w.ImageUrl,
                    AudioUrl = w.AudioUrl,
                    DifficultyLevel = w.DifficultyLevel.ToString(),
                    CategoryId = w.CategoryId,
                    CategoryName = w.CategoryEntity != null ? w.CategoryEntity.Name : w.Category
                })
                .ToListAsync(cancellationToken);
        }
    }
} 