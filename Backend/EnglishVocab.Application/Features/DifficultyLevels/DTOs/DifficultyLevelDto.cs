namespace EnglishVocab.Application.Features.DifficultyLevels.DTOs
{
    public class DifficultyLevelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public int WordCount { get; set; }
    }
} 