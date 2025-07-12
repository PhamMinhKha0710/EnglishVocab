using EnglishVocab.Domain.Entities;

namespace EnglishVocab.Application.Common.Models
{
    public class UserProgressWithWordInfo
    {
        public UserProgress Progress { get; set; }
        public string WordText { get; set; }
        public string WordTranslation { get; set; }
        public string Category { get; set; }
        public string DifficultyLevel { get; set; }
    }
} 