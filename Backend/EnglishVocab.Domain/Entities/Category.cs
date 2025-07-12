using EnglishVocab.Domain.Common;
using System;
using System.Collections.Generic;

namespace EnglishVocab.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        
        // Navigation property
        public virtual ICollection<Word> Words { get; set; } = new List<Word>();
    }
} 