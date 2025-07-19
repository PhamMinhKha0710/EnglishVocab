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
            
            // Vô hiệu hóa tính năng tự tăng cho cột ID
            builder.Property(w => w.Id)
                .ValueGeneratedNever();
            
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

            builder.Property(w => w.Notes)
                .HasMaxLength(500);

            builder.Property(w => w.ImageUrl)
                .HasMaxLength(255);

            builder.Property(w => w.AudioUrl)
                .HasMaxLength(255);

            // Configure the relationship with Category
            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(w => w.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure the relationship with DifficultyLevel
            builder.HasOne<DifficultyLevel>()
                .WithMany()
                .HasForeignKey(w => w.DifficultyLevelId)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed data cho Word - PHẢI có ít nhất 2 từ với ID 1 và 2 để đảm bảo tham chiếu khóa ngoại từ bảng UserProgresses
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
                    CategoryId = 1, // Assuming Category ID for "Greetings"
                    DifficultyLevelId = (int)DifficultyLevelType.Easy,
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
                    CategoryId = 1, // Assuming Category ID for "Greetings"
                    DifficultyLevelId = (int)DifficultyLevelType.Easy,
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
                    CategoryId = 2, // Assuming Category ID for "Expressions"
                    DifficultyLevelId = (int)DifficultyLevelType.Easy,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                }
            );
        }
    }
} 