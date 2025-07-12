using System;

namespace EnglishVocab.Application.Features.StudyManagement.DTOs
{
    public class DailyProgressDto
    {
        public DateTime Date { get; set; }
        public int WordsStudied { get; set; }
        public int WordsKnown { get; set; }
        public int WordsUnknown { get; set; }
        public int TotalMinutes { get; set; }
        public int SessionCount { get; set; }
    }
} 