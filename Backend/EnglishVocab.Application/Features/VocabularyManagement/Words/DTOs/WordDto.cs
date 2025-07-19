using System;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs
{
    public class WordDto
    {
        public int Id { get; set; }
        public string English { get; set; }
        public string Vietnamese { get; set; }
        public string Pronunciation { get; set; }
        public string Example { get; set; }
        public string Notes { get; set; }
        public string ImageUrl { get; set; }
        public string AudioUrl { get; set; }
        
        // Foreign keys
        public int? CategoryId { get; set; }
        public int? DifficultyLevelId { get; set; }
        
        // Related information
        public string CategoryName { get; set; }
        public string DifficultyLevelName { get; set; }
        public int? DifficultyValue { get; set; }
        
        // Audit fields
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
} 