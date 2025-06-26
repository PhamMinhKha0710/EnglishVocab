using System.Collections.Generic;
using EnglishVocab.Domain.Enums;

namespace EnglishVocab.Domain.Entities
{
    public class Word : BaseEntity
    {
        public string English { get; set; } = string.Empty;
        public string Vietnamese { get; set; } = string.Empty;
        public string? Pronunciation { get; set; }
        public string? Example { get; set; }
        public string? Notes { get; set; }
        public string? ImageUrl { get; set; }
        public string? AudioUrl { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; } = DifficultyLevel.Medium;
        
        // Navigation properties
        public Guid? WordSetId { get; set; }
        public WordSet? WordSet { get; set; }
        public ICollection<UserProgress> UserProgress { get; set; } = new List<UserProgress>();
    }
} 