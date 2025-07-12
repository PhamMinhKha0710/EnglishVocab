using MediatR;
using System.Collections.Generic;
using EnglishVocab.Application.Features.WordSets.Queries.GetAllWordSets;

namespace EnglishVocab.Application.Features.WordSets.Queries.GetUserWordSets
{
    public class GetUserWordSetsQuery : IRequest<IEnumerable<WordSetDto>>
    {
        public string UserId { get; set; }
    }
} 