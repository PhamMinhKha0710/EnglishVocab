using System.Collections.Generic;

namespace EnglishVocab.Domain.Entities
{
    public class WordSet : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsPublic { get; set; } = false;
        
        // Navigation properties
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Word> Words { get; set; } = new List<Word>();
    }
} 