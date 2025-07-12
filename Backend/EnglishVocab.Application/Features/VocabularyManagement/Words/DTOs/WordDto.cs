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
        public string DifficultyLevel { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 