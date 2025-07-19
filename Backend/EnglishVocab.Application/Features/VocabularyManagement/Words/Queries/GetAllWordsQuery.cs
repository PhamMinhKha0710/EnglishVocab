using System;
using System.Collections.Generic;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetAllWordsQuery : IRequest<IEnumerable<WordDto>>
    {
        public string SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public int? DifficultyLevelId { get; set; }
    }
} 