using EnglishVocab.Domain.Common;
using System;
using System.Collections.Generic;

namespace EnglishVocab.Domain
{
    public class WordSet : BaseEntity
    {
        public string Name { get; set; } 
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPublic { get; set; } = false;
        
        // Navigation properties
        public long UserId { get; set; }
        public ICollection<Word> Words { get; set; } = new List<Word>();
        public ICollection<StudySession> StudySessions { get; set; } = new List<StudySession>();
    }
} 