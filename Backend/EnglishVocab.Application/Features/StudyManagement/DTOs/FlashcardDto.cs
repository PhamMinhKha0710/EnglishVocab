using System.Collections.Generic;

namespace EnglishVocab.Application.Features.StudyManagement.DTOs
{
    public class FlashcardDto
    {
        public int WordId { get; set; }
        public string Text { get; set; }
        public string Translation { get; set; }
        public string Pronunciation { get; set; }
        public List<string> Examples { get; set; } = new List<string>();
        public string Category { get; set; }
        public string DifficultyLevel { get; set; }
    }
} 