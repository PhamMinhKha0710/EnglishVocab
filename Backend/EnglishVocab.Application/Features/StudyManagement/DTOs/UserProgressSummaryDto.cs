using System;

namespace EnglishVocab.Application.Features.StudyManagement.DTOs
{
    public class UserProgressSummaryDto
    {
        public int TotalWordsStudied { get; set; }
        public int TotalSessions { get; set; }
        public int TotalTimeSpent { get; set; }
        public int MasteredWords { get; set; }
        public int LearningWords { get; set; }
        public int StreakDays { get; set; }
        public DateTime? LastStudyDate { get; set; }
    }
} 