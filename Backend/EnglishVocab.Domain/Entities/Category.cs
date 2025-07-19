using EnglishVocab.Domain.Common;
using System;
using System.Collections.Generic;

namespace EnglishVocab.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        // No navigation properties in domain entities per Clean Architecture
    }
} 