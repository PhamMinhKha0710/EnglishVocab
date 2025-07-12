using System;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.StudyManagement.DTOs
{
    public class UserStatisticsDto
    {
        public int TotalWordsStudied { get; set; }
        public int MasteredWords { get; set; }
        public int LearningWords { get; set; }
        public int TotalSessions { get; set; }
        public int TotalTimeSpent { get; set; } // In minutes
        public int StreakDays { get; set; }
        public DateTime? LastStudyDate { get; set; }
        public List<DailyProgressDto> DailyProgress { get; set; } = new List<DailyProgressDto>();
    }
} 