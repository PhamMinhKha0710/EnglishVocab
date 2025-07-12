using EnglishVocab.Domain.Common;
using System;

namespace EnglishVocab.Domain.Entities
{
    public class WordSetWord : BaseEntity
    {
        public int WordId { get; set; }
        public int WordSetId { get; set; }
        public DateTime AddedDate { get; set; }

        // Navigation properties
        public virtual Word Word { get; set; }
        public virtual WordSet WordSet { get; set; }
    }
} 