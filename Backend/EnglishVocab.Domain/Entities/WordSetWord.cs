using EnglishVocab.Domain.Common;
using System;

namespace EnglishVocab.Domain.Entities
{
    public class WordSetWord : BaseEntity
    {
        public int WordId { get; set; }
        public int WordSetId { get; set; }
        public DateTime AddedDate { get; set; }
    }
} 