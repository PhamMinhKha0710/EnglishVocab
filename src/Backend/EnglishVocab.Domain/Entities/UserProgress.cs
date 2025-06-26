using EnglishVocab.Domain.Enums;

namespace EnglishVocab.Domain.Entities
{
    public class UserProgress : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid WordId { get; set; }
        public MasteryLevel MasteryLevel { get; set; } = MasteryLevel.New;
        public int CorrectCount { get; set; } = 0;
        public int IncorrectCount { get; set; } = 0;
        public DateTime? LastReviewed { get; set; }
        public DateTime? NextReviewDate { get; set; }
        
        // Navigation properties
        public User? User { get; set; }
        public Word? Word { get; set; }
    }
} 