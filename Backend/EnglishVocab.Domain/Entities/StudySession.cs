using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Common;
using System;

namespace EnglishVocab.Domain
{
    public class StudySession : BaseEntity
    {
        public long UserId { get; set; }
        public long WordSetId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int WordsStudied { get; set; } = 0;
        public int CorrectAnswers { get; set; } = 0;
        public int IncorrectAnswers { get; set; } = 0;
        public StudyMode StudyMode { get; set; } = StudyMode.Flashcards;
        
        // Navigation properties
        public WordSet? WordSet { get; set; }
    }
} 