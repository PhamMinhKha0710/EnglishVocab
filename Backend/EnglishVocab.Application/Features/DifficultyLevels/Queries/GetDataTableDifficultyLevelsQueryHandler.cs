using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.DifficultyLevels.Queries
{
    public class GetDataTableDifficultyLevelsQueryHandler : IRequestHandler<GetDataTableDifficultyLevelsQuery, DataTableResponse<DifficultyLevelDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDataTableService _dataTableService;

        public GetDataTableDifficultyLevelsQueryHandler(IApplicationDbContext context, IDataTableService dataTableService)
        {
            _context = context;
            _dataTableService = dataTableService;
        }

        public async Task<DataTableResponse<DifficultyLevelDto>> Handle(GetDataTableDifficultyLevelsQuery request, CancellationToken cancellationToken)
        {
            // Tạo query cơ bản
            var query = _context.DifficultyLevels
                .Select(d => new DifficultyLevelDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Value = d.Value
                })
                .AsQueryable();

            // Định nghĩa các thuộc tính tìm kiếm
            Expression<Func<DifficultyLevelDto, string>>[] searchProperties = new Expression<Func<DifficultyLevelDto, string>>[] {
                d => d.Name,
                d => d.Description
            };

            // Áp dụng tìm kiếm toàn cục
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = _dataTableService.ApplySearch(query, request, searchProperties);
            }

            // Tạo response
            return await _dataTableService.CreateResponseAsync(query, request, cancellationToken);
        }
    }
} 