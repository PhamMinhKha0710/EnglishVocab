namespace EnglishVocab.Application.Features.StudyManagement.DTOs
{
    public class StudySessionProgressDto
    {
        public int SessionId { get; set; }
        public string Progress { get; set; }
        public int CompletedWords { get; set; }
        public int TotalWords { get; set; }
        public int PercentComplete { get; set; }
    }
} 
 