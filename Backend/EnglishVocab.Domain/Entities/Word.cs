using System;
using EnglishVocab.Domain.Common;

namespace EnglishVocab.Domain.Entities
{
    public class Word : BaseEntity
    {
        public string English { get; set; }
        public string Vietnamese { get; set; }
        public string Pronunciation { get; set; }
        public string Example { get; set; }
        public string Notes { get; set; }
        public string ImageUrl { get; set; }
        public string AudioUrl { get; set; }
        
        // Foreign keys
        public int? CategoryId { get; set; }
        public int? DifficultyLevelId { get; set; }
        
        // No navigation properties in domain entities per Clean Architecture
    }
} 