using EnglishVocab.Domain.Common;
using System;
using System.Collections.Generic;

namespace EnglishVocab.Domain.Entities
{
    public class WordSet : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsPublic { get; set; } = false;
        public int WordCount { get; set; } = 0;
        public bool IsDefault { get; set; } = false;
        public string ImageUrl { get; set; }
        
        // Additional fields
        public string CreatedByUserId { get; set; } // Changed to string to match AspNetUsers Id type
        
        // No navigation properties to follow Clean Architecture
    }
} 