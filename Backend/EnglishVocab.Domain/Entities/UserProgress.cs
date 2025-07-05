using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Common;
using System;

namespace EnglishVocab.Domain
{
    public class UserProgress : BaseEntity
    {
        public long UserId { get; set; }
        public long WordId { get; set; }
        public MasteryLevel MasteryLevel { get; set; } = MasteryLevel.NotStudied;
        public int CorrectCount { get; set; } = 0;
        public int IncorrectCount { get; set; } = 0;
        public DateTime? LastReviewed { get; set; }
        public DateTime? NextReviewDate { get; set; }
        
        // Navigation properties
        public Word? Word { get; set; }
    }
} 