using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetPaginatedWordsQuery : IRequest<DataTableResponse<WordDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Id";
        public bool Ascending { get; set; } = true;
        public string SearchTerm { get; set; }
        public string Category { get; set; }
        public string DifficultyLevel { get; set; }
    }
} 
 