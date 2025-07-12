using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Common;
using System;
using System.Collections.Generic;

namespace EnglishVocab.Domain.Entities
{
    public class Word : BaseEntity
    {
        public string English { get; set; }
        public string Vietnamese { get; set; }
        public string Pronunciation { get; set; }
        public string Example { get; set; }
        public string ExampleTranslation { get; set; }
        public string AudioUrl { get; set; }
        public string ImageUrl { get; set; }
        public DifficultyLevelType DifficultyLevel { get; set; }
        
        // Keep the string Category for backward compatibility
        public string? Category { get; set; }
        
        // Add relationship with Category entity
        public int? CategoryId { get; set; }
        public virtual Category CategoryEntity { get; set; }
        
        public int Frequency { get; set; } // Tần suất xuất hiện của từ, số càng thấp càng phổ biến
        
        public string Notes { get; set; }
    }
} 