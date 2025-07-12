using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using EnglishVocab.Constants.Constant;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class StudySessionConfiguration : IEntityTypeConfiguration<StudySession>
    {
        public void Configure(EntityTypeBuilder<StudySession> builder)
        {
            // Basic configurations
            builder.HasKey(ss => ss.Id);
            
            builder.Property(ss => ss.UserId)
                .HasMaxLength(450) // Match AspNetUsers.Id length
                .IsRequired();  // Explicitly mark as required
                
            builder.Property(ss => ss.Status)
                .HasMaxLength(20);
                
            // Define relationships in Infrastructure layer
            builder.HasOne<WordSet>()
                  .WithMany()
                  .HasForeignKey(ss => ss.WordSetId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // Seed data cho StudySession
            var now = DateTime.Now;
            builder.HasData(
                new StudySession
                {
                    Id = 1,
                    UserId = "1", // Sử dụng ID của admin user từ ApplicationUserConfiguration
                    WordSetId = 1,
                    StartTime = now.AddHours(-1),
                    EndTime = now.AddMinutes(-30),
                    WordsStudied = 10,
                    CorrectAnswers = 7,
                    IncorrectAnswers = 3,
                    StudyMode = StudyMode.Flashcards,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                },
                new StudySession
                {
                    Id = 2,
                    UserId = "1", // Sử dụng ID của admin user từ ApplicationUserConfiguration
                    WordSetId = 2,
                    StartTime = now.AddDays(-1),
                    EndTime = now.AddDays(-1).AddMinutes(45),
                    WordsStudied = 15,
                    CorrectAnswers = 12,
                    IncorrectAnswers = 3,
                    StudyMode = StudyMode.Quiz,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                }
            );
        }
    }
} 