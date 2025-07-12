using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.StudyManagement.Sessions.Queries.GetNextFlashcard
{
    public class GetNextFlashcardQuery : IRequest<FlashcardDto>
    {
        public int SessionId { get; set; }
    }
} 