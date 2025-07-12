using EnglishVocab.Application.Features.WordSets.Queries.GetAllWordSets;
using MediatR;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.WordSets.Queries.GetDefaultWordSets
{
    public class GetDefaultWordSetsQuery : IRequest<IEnumerable<WordSetDto>>
    {
        // Không cần tham số
    }
} 