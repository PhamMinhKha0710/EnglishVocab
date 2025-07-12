using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class UserStatisticsConfiguration : IEntityTypeConfiguration<UserStatistics>
    {
        public void Configure(EntityTypeBuilder<UserStatistics> builder)
        {
            builder.HasKey(us => us.Id);
            
            builder.Property(us => us.UserId)
                .IsRequired()
                .HasMaxLength(450); // Match AspNetUsers.Id length
            
            // Create unique index for UserId to ensure one statistics entry per user
            builder.HasIndex(us => us.UserId)
                .IsUnique();
                
            // Default values
            builder.Property(us => us.TotalWordsStudied)
                .HasDefaultValue(0);
                
            builder.Property(us => us.TotalSessions)
                .HasDefaultValue(0);
                
            builder.Property(us => us.StreakDays)
                .HasDefaultValue(0);
                
            builder.Property(us => us.CurrentStreak)
                .HasDefaultValue(0);
                
            builder.Property(us => us.LongestStreak)
                .HasDefaultValue(0);
        }
    }
} 