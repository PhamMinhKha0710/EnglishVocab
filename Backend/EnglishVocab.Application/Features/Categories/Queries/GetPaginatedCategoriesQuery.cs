using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.Categories.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.Categories.Queries
{
    public class GetPaginatedCategoriesQuery : IRequest<DataTableResponse<CategoryDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Name";
        public bool Ascending { get; set; } = true;
    }
} 