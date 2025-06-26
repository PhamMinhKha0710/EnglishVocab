using EnglishVocab.Domain.Enums;

namespace EnglishVocab.Domain.Entities
{
    public class StudySession : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int WordsStudied { get; set; } = 0;
        public int CorrectAnswers { get; set; } = 0;
        public int IncorrectAnswers { get; set; } = 0;
        public StudyMode StudyMode { get; set; } = StudyMode.Flashcards;
        
        // Navigation properties
        public User? User { get; set; }
    }
} 