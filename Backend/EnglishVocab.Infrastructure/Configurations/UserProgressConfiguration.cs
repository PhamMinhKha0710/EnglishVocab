using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class UserProgressConfiguration : IEntityTypeConfiguration<UserProgress>
    {
        public void Configure(EntityTypeBuilder<UserProgress> builder)
        {
            // Basic configurations
            builder.HasKey(up => up.Id);
            
            builder.Property(up => up.UserId)
                .HasMaxLength(450) // Match AspNetUsers.Id length
                .IsRequired();  // Explicitly mark as required
            
            // Define relationships in Infrastructure layer
            builder.HasOne<Word>()
                  .WithMany()
                  .HasForeignKey(up => up.WordId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // Create a unique index for UserId + WordId combination
            builder.HasIndex(up => new { up.UserId, up.WordId })
                  .IsUnique();
                  
            // Seed data cho UserProgress
            var now = DateTime.Now;
            builder.HasData(
                new UserProgress
                {
                    Id = 1,
                    UserId = "1", // Sử dụng ID của admin user từ ApplicationUserConfiguration
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
                    UserId = "1", // Sử dụng ID của admin user từ ApplicationUserConfiguration
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