using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Common;
using System;

namespace EnglishVocab.Domain.Entities
{
    public class UserProgress : BaseEntity
    {
        public string UserId { get; set; } // Changed to string to match AspNetUsers Id type
        public int WordId { get; set; }
        
        public MasteryLevel MasteryLevel { get; set; } = MasteryLevel.New;
        public int RepetitionCount { get; set; } = 0;
        public DateTime LastReviewed { get; set; } = DateTime.UtcNow;
        public DateTime NextReviewDate { get; set; } = DateTime.UtcNow;
        public int EaseFactorInPercentage { get; set; } = 250; // Spaced repetition ease factor (250 = 2.5)
        public int IntervalInDays { get; set; } = 0;
        public int TimesReviewed { get; set; } = 0;
        
        // Additional properties needed by repositories and configurations
        public DateTime? LastReviewedAt { get; set; } = DateTime.UtcNow;
        public int ReviewCount { get; set; } = 0;
        public int CorrectCount { get; set; } = 0;
        public int IncorrectCount { get; set; } = 0;
        
        // Success rate calculation
        public double SuccessRate 
        { 
            get 
            {
                if (CorrectCount + IncorrectCount == 0)
                    return 0;
                
                return (double)CorrectCount / (CorrectCount + IncorrectCount) * 100;
            }
        }
        
        // No navigation properties in Domain layer
    }
} 