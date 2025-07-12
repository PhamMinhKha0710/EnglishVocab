using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class SearchWordsQuery : IRequest<IEnumerable<WordDto>>
    {
        public string SearchTerm { get; set; } = "";
        public string Category { get; set; } = "";
        public string DifficultyLevel { get; set; } = "";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
} 