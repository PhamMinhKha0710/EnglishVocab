using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class WordConfiguration : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            // Basic configurations
            builder.HasKey(w => w.Id);
            
            builder.Property(w => w.English)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(w => w.Vietnamese)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(w => w.Pronunciation)
                .HasMaxLength(100);

            builder.Property(w => w.Example)
                .HasMaxLength(500);

            builder.Property(w => w.ExampleTranslation)
                .HasMaxLength(500);

            builder.Property(w => w.Notes)
                .HasMaxLength(500);

            builder.Property(w => w.ImageUrl)
                .HasMaxLength(255);

            builder.Property(w => w.AudioUrl)
                .HasMaxLength(255);

            // Configure the relationship with Category
            builder.HasOne(w => w.CategoryEntity)
                .WithMany(c => c.Words)
                .HasForeignKey(w => w.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed data cho Word
            builder.HasData(
                new Word
                {
                    Id = 1,
                    English = "Hello",
                    Vietnamese = "Xin chào",
                    Pronunciation = "həˈloʊ",
                    Example = "Hello, how are you today?",
                    ExampleTranslation = "Xin chào, hôm nay bạn khỏe không?",
                    Notes = "Common greeting",
                    ImageUrl = "/images/hello.jpg",
                    AudioUrl = "/audio/hello.mp3",
                    DifficultyLevel = DifficultyLevelType.Easy,
                    Category = "Greetings",
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
                    ExampleTranslation = "Tạm biệt, hẹn gặp lại ngày mai!",
                    Notes = "Common farewell",
                    ImageUrl = "/images/goodbye.jpg",
                    AudioUrl = "/audio/goodbye.mp3",
                    DifficultyLevel = DifficultyLevelType.Easy,
                    Category = "Greetings",
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
                    ExampleTranslation = "Cảm ơn vì sự giúp đỡ của bạn.",
                    Notes = "Common expression of gratitude",
                    ImageUrl = "/images/thankyou.jpg",
                    AudioUrl = "/audio/thankyou.mp3",
                    DifficultyLevel = DifficultyLevelType.Easy,
                    Category = "Expressions",
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                }
            );
        }
    }
} 