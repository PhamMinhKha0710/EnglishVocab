using System;

namespace EnglishVocab.Application.Features.WordSets.Queries.GetAllWordSets
{
    public class WordSetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalWords { get; set; }
        public string Category { get; set; }
        public string DifficultyLevel { get; set; }
        public bool IsDefault { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
} 