using EnglishVocab.Constants.Constant;
using System;

namespace EnglishVocab.Application.Features.StudyManagement.DTOs
{
    public class WordProgressDto
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public string Word { get; set; }
        public string Translation { get; set; }
        public string Category { get; set; }
        public string DifficultyLevel { get; set; }
        public string MasteryLevel { get; set; }
        public int CorrectCount { get; set; }
        public int IncorrectCount { get; set; }
        public DateTime LastReviewed { get; set; }
        public DateTime NextReviewDate { get; set; }
        public DateTime NextReviewDue { get; set; }
        public int ReviewCount { get; set; }
        public int EaseFactorInPercentage { get; set; }
        public int IntervalInDays { get; set; }
        public double SuccessRate { get; set; }
    }
} 