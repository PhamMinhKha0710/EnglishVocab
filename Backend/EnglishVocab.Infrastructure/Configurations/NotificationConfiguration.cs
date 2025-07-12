using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);
            
            builder.Property(n => n.UserId)
                .IsRequired()
                .HasMaxLength(450); // Match AspNetUsers.Id length
                
            builder.Property(n => n.Message)
                .IsRequired()
                .HasMaxLength(500);
                
            builder.Property(n => n.Type)
                .IsRequired()
                .HasMaxLength(50);
                
            builder.Property(n => n.RedirectUrl)
                .HasMaxLength(500);
                
            // Create index for faster lookup by user
            builder.HasIndex(n => n.UserId);
            
            // Create index for unread notifications
            builder.HasIndex(n => new { n.UserId, n.IsRead });
        }
    }
} 