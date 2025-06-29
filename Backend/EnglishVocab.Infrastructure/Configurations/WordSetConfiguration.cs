using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class WordSetConfiguration : IEntityTypeConfiguration<WordSet>
    {
        public void Configure(EntityTypeBuilder<WordSet> builder)
        {
            // Seed data cho WordSet
            builder.HasData(
                new WordSet
                {
                    Id = 1,
                    Name = "Greetings and Basic Phrases",
                    Description = "Essential greetings and phrases for beginners",
                    Category = "Beginner",
                    IsPublic = true,
                    UserId = 1,
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
                    UserId = 1,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                }
            );
        }
    }
} 