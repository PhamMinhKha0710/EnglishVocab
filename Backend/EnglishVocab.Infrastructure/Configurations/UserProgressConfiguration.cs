using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class UserProgressConfiguration : IEntityTypeConfiguration<UserProgress>
    {
        public void Configure(EntityTypeBuilder<UserProgress> builder)
        {
            // Seed data cho UserProgress
            var now = DateTime.Now;
            builder.HasData(
                new UserProgress
                {
                    Id = 1,
                    UserId = 1,
                    WordId = 1,
                    MasteryLevel = MasteryLevel.Learning,
                    CorrectCount = 2,
                    IncorrectCount = 1,
                    LastReviewed = now.AddDays(-1),
                    NextReviewDate = now.AddDays(1),
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                },
                new UserProgress
                {
                    Id = 2,
                    UserId = 1,
                    WordId = 2,
                    MasteryLevel = MasteryLevel.NotStudied,
                    CorrectCount = 0,
                    IncorrectCount = 0,
                    NextReviewDate = now,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                }
            );
        }
    }
} 