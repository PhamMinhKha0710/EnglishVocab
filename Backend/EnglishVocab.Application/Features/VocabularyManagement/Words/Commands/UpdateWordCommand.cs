using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Commands
{
    public class UpdateWordCommand : IRequest<WordDto>
    {
        public int Id { get; set; }
        public string English { get; set; }
        public string Vietnamese { get; set; }
        public string Pronunciation { get; set; }
        public string Example { get; set; }
        public string ExampleTranslation { get; set; }
        public string Category { get; set; }
        public string DifficultyLevel { get; set; }
        public string AudioUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Notes { get; set; }
    }
} 
 