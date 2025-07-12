using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class WordSetConfiguration : IEntityTypeConfiguration<WordSet>
    {
        public void Configure(EntityTypeBuilder<WordSet> builder)
        {
            // Basic configurations
            builder.HasKey(ws => ws.Id);
            
            builder.Property(ws => ws.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ws => ws.Description)
                .HasMaxLength(500);

            builder.Property(ws => ws.Category)
                .HasMaxLength(50);

            builder.Property(ws => ws.ImageUrl)
                .HasMaxLength(255);
            
            builder.Property(ws => ws.CreatedByUserId)
                .HasMaxLength(450); // Match AspNetUsers.Id length
                
            // Seed data cho WordSet
            builder.HasData(
                new WordSet
                {
                    Id = 1,
                    Name = "Greetings and Basic Phrases",
                    Description = "Essential greetings and phrases for beginners",
                    Category = "Beginner",
                    IsPublic = true,
                    ImageUrl = "/images/wordsets/greetings.jpg",
                    WordCount = 3, // Updated to match the number of seeded words
                    CreatedByUserId = "1", // Admin user ID
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                },
                new WordSet
                {
                    Id = 2,
                    Name = "Food and Dining",
                    Description = "Vocabulary related to food, restaurants, and dining",
                    Category = "Intermediate",
                    IsPublic = true,
                    ImageUrl = "/images/wordsets/food.jpg",
                    WordCount = 0, // No words yet
                    CreatedByUserId = "1", // Admin user ID
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                }
            );
        }
    }
} 