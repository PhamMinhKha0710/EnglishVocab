using EnglishVocab.Domain.Common;
using System;

namespace EnglishVocab.Domain.Entities
{
    public class UserStatistics : BaseEntity
    {
        public string UserId { get; set; } // Changed to string to match AspNetUsers Id type
        public int TotalWordsStudied { get; set; }
        public int TotalSessions { get; set; }
        public int TotalTimeSpent { get; set; }
        public int MasteredWords { get; set; }
        public int LearningWords { get; set; }
        public int StreakDays { get; set; }
        public DateTime? LastStudyDate { get; set; }
        
        // Additional properties needed by the handler
        public int TotalPoints { get; set; }
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public int TotalWordsLearned { get; set; }
        public int TotalTimeSpentMinutes { get; set; }
        public DateTime LastActivity { get; set; }
        public DateTime StreakUpdatedAt { get; set; }
        
        // No navigation properties in Domain layer
    }
} 
 