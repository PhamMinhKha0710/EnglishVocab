using EnglishVocab.Domain.Common;
using System;

namespace EnglishVocab.Domain.Entities
{
    public class UserAchievement : BaseEntity
    {
        public string UserId { get; set; } // Changed to string to match AspNetUsers Id type
        public string AchievementType { get; set; } // streak, words_learned, points
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; } // Giá trị thành tích (ví dụ: 7 cho streak 7 ngày)
        public DateTime UnlockedAt { get; set; } = DateTime.UtcNow;
        public bool IsNew { get; set; } = true;
        public string IconUrl { get; set; }
    }
} 
 