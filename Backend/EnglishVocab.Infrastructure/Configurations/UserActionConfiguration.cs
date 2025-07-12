using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class UserActionConfiguration : IEntityTypeConfiguration<UserAction>
    {
        public void Configure(EntityTypeBuilder<UserAction> builder)
        {
            builder.HasKey(ua => ua.Id);
            
            builder.Property(ua => ua.UserId)
                .HasMaxLength(450); // Match AspNetUsers.Id length
                
            builder.Property(ua => ua.ActionType)
                .IsRequired()
                .HasMaxLength(50);
                
            builder.Property(ua => ua.EntityType)
                .HasMaxLength(100);
                
            builder.Property(ua => ua.Description)
                .HasMaxLength(500);
            
            // Relationships can be defined here if needed
            // For example, if we want to fetch related entities for reporting
            
            // StudySession relationship (optional)
            builder.HasOne<StudySession>()
                  .WithMany()
                  .HasForeignKey(ua => ua.StudySessionId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .IsRequired(false);
                  
            // Word relationship (optional)
            builder.HasOne<Word>()
                  .WithMany()
                  .HasForeignKey(ua => ua.WordId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .IsRequired(false);
        }
    }
} 