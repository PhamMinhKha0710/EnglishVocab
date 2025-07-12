using MediatR;
using System.Collections.Generic;
using EnglishVocab.Application.Features.VocabularyManagement.WordSets.DTOs;

namespace EnglishVocab.Application.Features.VocabularyManagement.WordSets.Queries.GetAllWordSets
{
    public class GetAllWordSetsQuery : IRequest<IEnumerable<WordSetDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Name";
        public bool Ascending { get; set; } = true;
    }
} 