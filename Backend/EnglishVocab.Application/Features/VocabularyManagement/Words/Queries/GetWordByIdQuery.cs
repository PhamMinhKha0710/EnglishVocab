using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetWordByIdQuery : IRequest<WordDto>
    {
        public int Id { get; set; }
    }
} 