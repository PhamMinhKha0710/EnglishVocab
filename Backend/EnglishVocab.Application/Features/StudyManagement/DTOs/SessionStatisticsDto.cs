namespace EnglishVocab.Application.Features.StudyManagement.DTOs
{
    public class SessionStatisticsDto
    {
        public int SessionId { get; set; }
        public string Time { get; set; }
        public int Known { get; set; }
        public int NeedReview { get; set; }
        public string Progress { get; set; }
        public int TotalWords { get; set; }
        public int CompletedWords { get; set; }
    }
} 