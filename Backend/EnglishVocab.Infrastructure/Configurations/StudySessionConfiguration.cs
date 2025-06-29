using EnglishVocab.Domain;
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
            // Seed data cho StudySession
            var now = DateTime.Now;
            builder.HasData(
                new StudySession
                {
                    Id = 1,
                    UserId = 1,
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
                    UserId = 1,
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