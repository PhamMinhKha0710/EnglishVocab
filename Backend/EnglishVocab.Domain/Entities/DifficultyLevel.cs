using System;
using EnglishVocab.Domain.Common;

namespace EnglishVocab.Domain.Entities
{
    public class DifficultyLevel : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }

        
        // No navigation properties in domain entities per Clean Architecture
    }
} 