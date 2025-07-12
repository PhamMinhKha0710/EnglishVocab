using EnglishVocab.Domain.Common;
using System;

namespace EnglishVocab.Domain.Entities
{
    public class UserAction : BaseEntity
    {
        public string UserId { get; set; } // Changed to string to match AspNetUsers Id type
        public int? StudySessionId { get; set; }
        public int? WordId { get; set; }
        
        public string ActionType { get; set; } // "known", "unknown", "skipped"
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int? ResponseTimeMs { get; set; } // Thời gian phản hồi tính bằng millisecond
        
        // Simplified properties
        public string EntityType { get; set; } // "Word", "WordSet", "StudySession"
        public int? EntityId { get; set; }
        public string Description { get; set; }
        
        // No navigation properties in Domain layer
    }
} 