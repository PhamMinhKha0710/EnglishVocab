using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Common;
using System;
using System.Collections.Generic;

namespace EnglishVocab.Domain
{
    public class Word : BaseEntity
    {
        public string English { get; set; } 
        public string Vietnamese { get; set; } 
        public string? Pronunciation { get; set; }
        public string? Example { get; set; }
        public string? Notes { get; set; }
        public string? ImageUrl { get; set; }
        public string? AudioUrl { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; } = DifficultyLevel.Medium;
        
        // Navigation properties
        public long? WordSetId { get; set; }
        public WordSet? WordSet { get; set; }
        public ICollection<UserProgress> UserProgress { get; set; } = new List<UserProgress>();
    }
} 