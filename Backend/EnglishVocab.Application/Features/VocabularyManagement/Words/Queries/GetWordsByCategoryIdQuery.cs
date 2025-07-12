using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetWordsByCategoryIdQuery : IRequest<IEnumerable<WordDto>>
    {
        public int CategoryId { get; set; }
    }
} 