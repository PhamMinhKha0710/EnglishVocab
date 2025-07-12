using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetDataTableWordsQueryHandler : IRequestHandler<GetDataTableWordsQuery, DataTableResponse<WordDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDataTableService _dataTableService;

        public GetDataTableWordsQueryHandler(IApplicationDbContext context, IDataTableService dataTableService)
        {
            _context = context;
            _dataTableService = dataTableService;
        }

        public async Task<DataTableResponse<WordDto>> Handle(GetDataTableWordsQuery request, CancellationToken cancellationToken)
        {
            // Tạo query cơ bản
            var query = _context.Words
                .AsQueryable();

            // Áp dụng các bộ lọc đặc biệt
            if (request.CategoryId.HasValue)
            {
                query = query.Where(w => w.CategoryId == request.CategoryId.Value);
            }

            if (!string.IsNullOrEmpty(request.DifficultyLevel))
            {
                query = query.Where(w => w.DifficultyLevel.ToString() == request.DifficultyLevel);
            }

            // Chuyển đổi sang DTO
            var dtoQuery = query
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
                });

            // Định nghĩa các thuộc tính tìm kiếm
            Expression<Func<WordDto, string>>[] searchProperties = {
                w => w.English,
                w => w.Vietnamese,
                w => w.Example,
                w => w.Notes,
                w => w.CategoryName
            };

            // Áp dụng tìm kiếm toàn cục
            if (!string.IsNullOrEmpty(request.Search))
            {
                dtoQuery = _dataTableService.ApplySearch(dtoQuery, request, searchProperties);
            }

            // Tạo response
            return await _dataTableService.CreateResponseAsync(dtoQuery, request, cancellationToken);
        }
    }
} 