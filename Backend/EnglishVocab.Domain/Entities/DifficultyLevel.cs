using EnglishVocab.Domain.Common;
using System.Collections.Generic;

namespace EnglishVocab.Domain.Entities
{
    public class DifficultyLevel : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        
        // Navigation properties
        public virtual ICollection<Word> Words { get; set; } = new List<Word>();
    }
} 