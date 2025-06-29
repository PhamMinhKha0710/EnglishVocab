using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class WordConfiguration : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            // Seed data cho Word
            builder.HasData(
                new Word
                {
                    Id = 1,
                    English = "Hello",
                    Vietnamese = "Xin chào",
                    Pronunciation = "həˈloʊ",
                    Example = "Hello, how are you today?",
                    Notes = "Common greeting",
                    ImageUrl = "/images/hello.jpg",
                    AudioUrl = "/audio/hello.mp3",
                    DifficultyLevel = DifficultyLevel.Easy,
                    WordSetId = 1,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                },
                new Word
                {
                    Id = 2,
                    English = "Goodbye",
                    Vietnamese = "Tạm biệt",
                    Pronunciation = "ɡʊdˈbaɪ",
                    Example = "Goodbye, see you tomorrow!",
                    Notes = "Common farewell",
                    ImageUrl = "/images/goodbye.jpg",
                    AudioUrl = "/audio/goodbye.mp3",
                    DifficultyLevel = DifficultyLevel.Easy,
                    WordSetId = 1,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                },
                new Word
                {
                    Id = 3,
                    English = "Thank you",
                    Vietnamese = "Cảm ơn",
                    Pronunciation = "θæŋk ju",
                    Example = "Thank you for your help.",
                    Notes = "Common expression of gratitude",
                    ImageUrl = "/images/thankyou.jpg",
                    AudioUrl = "/audio/thankyou.mp3",
                    DifficultyLevel = DifficultyLevel.Easy,
                    WordSetId = 1,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                }
            );
        }
    }
} 