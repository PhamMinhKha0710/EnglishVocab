using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Common;
using System;

namespace EnglishVocab.Domain.Entities
{
    public class StudySession : BaseEntity
    {
        public string UserId { get; set; } // Changed to string to match AspNetUsers Id type
        public int WordSetId { get; set; }
        
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int WordsStudied { get; set; }
        public int WordsKnown { get; set; }
        public int WordsUnknown { get; set; }
        public int WordsSkipped { get; set; }
        public int PointsEarned { get; set; }
        
        public StudyMode StudyMode { get; set; }
        public bool ShuffleWords { get; set; }
        public string Status { get; set; } = "active"; // active, paused, completed
        
        // Additional properties
        public int CorrectAnswers { get; set; } = 0;
        public int IncorrectAnswers { get; set; } = 0;
        
        // Calculated properties
        public TimeSpan Duration => EndTime.HasValue ? EndTime.Value - StartTime : DateTime.UtcNow - StartTime;
        
        // No navigation properties in Domain layer
    }
} 