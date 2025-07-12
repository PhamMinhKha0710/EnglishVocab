using System;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.StudyManagement.DTOs
{
    public class StudySessionDto
    {
        public int Id { get; set; }
        public int WordSetId { get; set; }
        public string WordSetName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Status { get; set; }
        public string StudyMode { get; set; }
        public int WordsStudied { get; set; }
        public int WordsKnown { get; set; }
        public int WordsUnknown { get; set; }
        public double AccuracyRate { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
} 