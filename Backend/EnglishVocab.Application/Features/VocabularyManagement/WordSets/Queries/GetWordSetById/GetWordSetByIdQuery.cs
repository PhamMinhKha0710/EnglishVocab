using EnglishVocab.Application.Features.VocabularyManagement.WordSets.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.WordSets.Queries.GetWordSetById
{
    public class GetWordSetByIdQuery : IRequest<WordSetDetailDto>
    {
        public int Id { get; set; }
    }
} 